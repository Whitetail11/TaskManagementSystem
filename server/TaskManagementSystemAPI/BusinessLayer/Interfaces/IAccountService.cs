using BusinessLayer.Classes;
using BusinessLayer.DTOs;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResult> CreateUser(CreateUserDTO createUserDTO, bool registration = false);
        Task<AccountResult> Register(RegisterDTO registerDTO);
        Task<AccountResult> Login(LoginDTO loginDTO);
        Task<AccountResult> GetAllUsers();
    }
}
