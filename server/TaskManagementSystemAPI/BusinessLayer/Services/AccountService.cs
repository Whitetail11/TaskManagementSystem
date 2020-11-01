using AutoMapper;
using BusinessLayer.Classes;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataLayer.Classes;
using DataLayer.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class AccountService: IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthOptions _authOptions;
        private readonly IMapper _mapper;

        public AccountService(UserManager<ApplicationUser> userManager, IOptions<AuthOptions> options,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authOptions = options.Value;
        }
        public async Task<IEnumerable<GetUserDTO>> GetAllUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("Executor");
            var res = users.Select(src =>
                new GetUserDTO { Id = src.Id, Name = src.Name, Surname = src.Surname, Email = src.Email })
                .ToList();
            return res;
        }
        public async Task<AccountResult> CreateUser(CreateUserDTO createUserDTO, bool registration = false)
        {
            var user = new ApplicationUser()
            {
                Name = createUserDTO.Name,
                Surname = createUserDTO.Surname,
                Email = createUserDTO.Email,
                UserName = createUserDTO.Email
            };

            var result = await _userManager.CreateAsync(user, createUserDTO.Password);
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, createUserDTO.Role);
                if (registration)
                {
                    return new AccountResult(true, user);
                }
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

        public async Task<AccountResult> Register(RegisterDTO registerDTO)
        {
            var createUser = new CreateUserDTO()
            {
                Name = registerDTO.Name,
                Surname = registerDTO.Surname,
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                PasswordConfirm = registerDTO.Password,
                Role = ApplicationConstants.Roles.EXECUTOR
            };

            var result = await CreateUser(createUser, true);
            if (!result.Succeeded)
            {
                return result;
            }
            
            var token = await GenerateJWT(result.User);
            return new AccountResult(true, token);
        }

        public async Task<AccountResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                var token = await GenerateJWT(user);
                return new AccountResult(true, token);
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
                new Claim("userid", user.Id),
                new Claim("useremail", user.Email)
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Count != 0)
            {
                claims.Add(new Claim("role", userRoles.First()));
            }
            return claims;
        }

        private async Task<string> GenerateJWT(ApplicationUser user)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(_authOptions.Lifetime)),
                claims: await GetUserClaims(user),
                signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<IEnumerable<ApplicationUser>> GetUserForRole(string role)
        {
            return await _userManager.GetUsersInRoleAsync(role);
        }

        public async Task<IEnumerable<SelectUserDTO>> GetUsersForSelect(string role)
        {
            var users = await GetUserForRole(role);
            return _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<SelectUserDTO>>(users);
        }
    }
}
