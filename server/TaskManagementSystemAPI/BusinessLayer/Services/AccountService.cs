using BusinessLayer.Classes;
using BusinessLayer.ViewModels;
using DataLayer.Classes;
using DataLayer.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthOptions _authOptions;

        public AccountService(UserManager<ApplicationUser> userManager, IOptions<AuthOptions> options)
        {
            _userManager = userManager;
            _authOptions = options.Value;
        }

        public async Task<AccountResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, ApplicationConstants.Roles.EXECUTOR);

                return new AccountResult(true);
            }
            else
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description.Replace("User name", "Email"));
                }

                return new AccountResult(errors);
            }
        }

        public async Task<AccountResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromDays(_authOptions.Lifetime)),
                    claims: await GetUserClaims(user),
                    signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return new AccountResult(true, encodedJwt);
            }
            else
            {
                return new AccountResult(new List<string>() { "Invalid email or password." });
            }
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Count != 0)
            {
                claims.Add(new Claim("role", userRoles.First()));
            }

            return claims;
        }
    }
}
