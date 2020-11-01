using BusinessLayer.Classes;
using BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResult> CreateUser(CreateUserDTO createUserDTO, bool registration = false);
        Task<AccountResult> Register(RegisterDTO registerDTO);
        Task<AccountResult> Login(LoginDTO loginDTO);
        Task<IEnumerable<GetUserDTO>> GetAllUsers();
        Task<IEnumerable<SelectUserDTO>> GetUsersForSelect(string role);
    }
}
