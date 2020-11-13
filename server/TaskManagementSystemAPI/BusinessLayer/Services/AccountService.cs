﻿using AutoMapper;
using BusinessLayer.Classes;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
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
using System.Threading.Tasks;
using System.Web;

namespace BusinessLayer.Services
{
    public class AccountService: IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthOptions _authOptions;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public AccountService(UserManager<ApplicationUser> userManager, IOptions<AuthOptions> options,
            IMapper mapper, INotificationService notificationService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authOptions = options.Value;
            _notificationService = notificationService;
        }

        public async Task<ShowUserDTO> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<ApplicationUser, ShowUserDTO>(user);
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
                await SendEmailConfirmationLink(user);
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

        public async Task<AccountResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new AccountResult(new List<string>() { "Email address confirmation link is invalid." });
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                //var token = await GenerateJWT(user);
                return new AccountResult(true);
            }
            else
            {
                return new AccountResult(new List<string>() { "Email address confirmation link is invalid." });
            }
        }

        public async Task SendEmailConfirmationLink(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await SendEmailConfirmationLink(user);
        }

        private async Task SendEmailConfirmationLink(ApplicationUser user)
        {
            var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedCode = HttpUtility.UrlEncode(confirmationCode);
            var confirmationLink = new Uri($"http://localhost:4200/confirm-email?userId={user.Id}&code={encodedCode}");
            _notificationService.SendEmailAsync(user.Email, "Confirm email address",
                $"In order to complete the confirmation of the email address, follow the <a href='{confirmationLink}'>link</a>.");
        }

        public async Task ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return;
            }

            var resetCode = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedCode = HttpUtility.UrlEncode(resetCode);
            var passwordResetLink = new Uri($"http://localhost:4200/reset-password?userId={user.Id}&code={encodedCode}");
            _notificationService.SendEmailAsync(user.Email, "Reset password",
                $"In order to reset your password, follow the <a href='{passwordResetLink}'>link</a>.");
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim("userid", user.Id),
                new Claim("useremail", user.Email),
                //new Claim("useremailconfirmed", user.EmailConfirmed.ToString().ToLower())
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

        public async Task<bool> EmailConfirmed(string id)
        {
            var users = await _userManager.GetUsersInRoleAsync("Executor");
            var res = users.FirstOrDefault(src => src.Id == id);
            return res.EmailConfirmed;
        }
    }
}
