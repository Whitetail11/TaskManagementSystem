using BusinessLayer.Classes;
using BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task<ShowUserDTO> Get(string id);
        Task<AccountResult> CreateUser(CreateUserDTO createUserDTO, bool registration = false);
        Task<AccountResult> Register(RegisterDTO registerDTO);
        Task<AccountResult> Login(LoginDTO loginDTO);
        Task<AccountResult> ConfirmEmail(string userId, string code);
        Task SendEmailConfirmationLink(string userId);
        Task ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Task<IEnumerable<SelectUserDTO>> GetUsersForSelect(string role);
        Task<bool> EmailConfirmed(string id);
    }
}
