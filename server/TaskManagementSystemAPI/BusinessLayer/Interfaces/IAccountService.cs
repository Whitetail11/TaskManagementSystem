using BusinessLayer.Classes;
using BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task<ShowUserDTO> GetUserById(string id);
        Task<IEnumerable<ShowListUserDTO>> GetForPage(PageDTO pageDTO);
        Task<int> GetUserCount();
        Task<AccountResult> CreateUser(CreateUserDTO createUserDTO);
        Task<AccountResult> UpdateUser(string id, UpdateUserDTO updateUserDTO);
        Task<AccountResult> Register(RegisterDTO registerDTO);
        Task<AccountResult> Login(LoginDTO loginDTO);
        Task SendEmailConfirmationLink(string userId);
        Task<AccountResult> ConfirmEmail(ConfirmEmailDTO confirmEmailDTO);
        Task ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Task<AccountResult> ResetPassword(ResetPasswordDTO resetPasswordDTO);
        Task<AccountResult> ChangePassword(string userId, ChangePasswordDTO changePasswordDTO);
        Task<IEnumerable<SelectUserDTO>> GetUsersForSelect(string role);
        Task<bool> IsEmailConfirmed(string email);
        string GetFullName(string userId);
        Task<bool> ExistAnyUserWithId(string id);
        Task<AccountResult> DeleteUser(string id, DeleteUserDTO deleteUserDTO);
    }
}
