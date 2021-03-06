﻿using AutoMapper;
using BusinessLayer.Classes;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataLayer.Classes;
using DataLayer.Identity;
using Microsoft.Extensions.Configuration;
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
        private readonly ApplicationUserManager _userManager;
        private readonly AuthOptions _authOptions;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly string _clientAppUrl;

        public AccountService(ApplicationUserManager userManager, IOptions<AuthOptions> options, IMapper mapper,
            INotificationService notificationService, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authOptions = options.Value;
            _notificationService = notificationService;
            _clientAppUrl = configuration.GetValue<string>("ClientAppUrl");
        }

        public async Task<ShowUserDTO> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<ApplicationUser, ShowUserDTO>(user);
        }

        public async Task<IEnumerable<ShowListUserDTO>> GetForPage(PageDTO pageDTO)
        {
            var page = _mapper.Map<PageDTO, Page>(pageDTO);
            var users = await _userManager.GetForPage(page);
            return _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ShowListUserDTO>>(users);
        }

        public async Task<int> GetUserCount()
        {
            return await _userManager.GetCountAsync();
        }

        public async Task<AccountResult> CreateUser(CreateUserDTO createUserDTO)
        {
            return await CreateUser(createUserDTO, RandomPasswordGenerator.GenerateRandomPassword());
        }

        public async Task<AccountResult> Register(RegisterDTO registerDTO)
        {
            var createUser = new CreateUserDTO()
            {
                Name = registerDTO.Name,
                Surname = registerDTO.Surname,
                Email = registerDTO.Email,
                Role = ApplicationConstants.Roles.EXECUTOR
            };

            var result = await CreateUser(createUser, registerDTO.Password, true);
            if (!result.Succeeded)
            {
                return result;
            }
            
            var token = await GenerateJWT(result.User);
            return new AccountResult(true, token);
        }

        private async Task<AccountResult> CreateUser(CreateUserDTO createUserDTO, string password, bool registration = false)
        {
            var user = new ApplicationUser()
            {
                Name = createUserDTO.Name,
                Surname = createUserDTO.Surname,
                Email = createUserDTO.Email,
                UserName = createUserDTO.Email,
                Date = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, createUserDTO.Role);

                if (!registration)
                {
                    SendPasswordToUserEmail(user.Email, password);
                }

                await SendEmailConfirmationLink(user);
                
                if (registration)
                {
                    return new AccountResult(true, user);
                }
                return new AccountResult(true);
            }
            
            var errors = result.Errors.Select(error => error.Description.Replace("User name", "Email"));
            return new AccountResult(errors);
        }

        public async Task<AccountResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                var token = await GenerateJWT(user);
                return new AccountResult(true, token);
            }
            
            return new AccountResult(new List<string>() { "Invalid email or password." });
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
            var confirmationLink = new Uri($"{_clientAppUrl}confirm-email?userId={user.Id}&code={encodedCode}");
            _notificationService.SendEmailAsync(user.Email, "Confirm email address",
                $"In order to complete the confirmation of the email address, follow the <a href='{confirmationLink}'>link</a>.");
        }

        public void SendPasswordToUserEmail(string email, string password)
        {
            var loginPageLink = new Uri($"{_clientAppUrl}login");
            _notificationService.SendEmailAsync(email, "Task Management System Account",
                $"Account has been created in <a href='{loginPageLink}'>Task Management System</a> for you.<br />" +
                $"Your login: { email } <br />" +
                $"Your password: { password }");
        }

        public async Task<AccountResult> ConfirmEmail(ConfirmEmailDTO confirmEmailDTO)
        {
            var user = await _userManager.FindByIdAsync(confirmEmailDTO.UserId);
            if (user == null)
            {
                return new AccountResult(new List<string>() { "Error: email address confirmation link was invalid." });
            }

            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDTO.Code);
            if (result.Succeeded)
            {
                //var token = await GenerateJWT(user);
                return new AccountResult(true);
            }
            
            return new AccountResult(new List<string>() { "Error: email address confirmation link was invalid." });
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
            var passwordResetLink = new Uri($"{_clientAppUrl}reset-password?userId={user.Id}&code={encodedCode}");
            _notificationService.SendEmailAsync(user.Email, "Reset password",
                $"In order to reset your password, follow the <a href='{passwordResetLink}'>link</a>.");
        }

        public async Task<AccountResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByIdAsync(resetPasswordDTO.UserId);
            if (user == null)
            {
                return new AccountResult(new List<string>() { "Error: password reset link was invalid." });
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.Code, resetPasswordDTO.Password);
            if (result.Succeeded)
            {
                return new AccountResult(true);
            } 
            
            var errors = result.Errors.Select(error => error.Description.Replace("Invalid token.", "Error: password reset link was invalid."));
            return new AccountResult(errors);
        }

        public async Task<AccountResult> ChangePassword(string userId, ChangePasswordDTO changePasswordDTO)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);
            
            if (result.Succeeded)
            {
                return new AccountResult(true);
            }
            
            return new AccountResult(result.Errors.Select(error => error.Description).ToList());
        }

        public async Task<AccountResult> UpdateUser(string id, UpdateUserDTO updateUserDTO)
        {
            var user = await _userManager.FindByIdAsync(id);
            var emailChanged = user.Email != updateUserDTO.Email;

            user.Name = updateUserDTO.Name;
            user.Surname = updateUserDTO.Surname;
            user.Email = updateUserDTO.Email;
            user.UserName = updateUserDTO.Email;
            var result = await _userManager.UpdateAsync(user);
            
            if (result.Succeeded)
            {
                if (user.EmailConfirmed && emailChanged)
                {
                    await _userManager.SetEmailAsNotConfirmedAsync(user);
                }
                return new AccountResult(true);
            }
            
            var errors = result.Errors.Select(error => error.Description.Replace("User name", "Email"));
            return new AccountResult(errors);
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

        private async Task<IEnumerable<ApplicationUser>> GetUsersForRole(string role)
        {
            return await _userManager.GetUsersInRoleAsync(role);
        }

        public async Task<IEnumerable<SelectUserDTO>> GetUsersForSelect(string role)
        {
            var users = await GetUsersForRole(role);
            return _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<SelectUserDTO>>(users);
        }

        public async Task<bool> IsEmailConfirmed(string email)
        {
            return (await _userManager.FindByEmailAsync(email)).EmailConfirmed;
        }

        public string GetFullName(string userId)
        {
            return _userManager.GetFullName(userId);
        }

        public async Task<bool> ExistAnyUserWithId(string id)
        {
            return await _userManager.ExistAnyAsync(id);
        }

        public async Task<AccountResult> DeleteUser(string id, DeleteUserDTO deleteUserDTO)
        {
            if (!(await _userManager.CheckPasswordAsync(await _userManager.FindByIdAsync(id), deleteUserDTO.Password)))
            {
                return new AccountResult(new List<string>() { "Invalid password." });
            }

            await _userManager.DeleteAsync(id);
            return new AccountResult(true);
        }
    }
}
