using BusinessLayer.Classes;
using BusinessLayer.ViewModels;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResult> Register(RegisterViewModel model);
        Task<AccountResult> Login(LoginViewModel model);
    }
}
