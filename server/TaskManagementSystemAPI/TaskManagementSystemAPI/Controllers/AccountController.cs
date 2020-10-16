using BusinessLayer.Classes;
using BusinessLayer.Services;
using BusinessLayer.ViewModels;
using DataLayer.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TaskManagementSystemAPI.Classes;

namespace TaskManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(UserManager<ApplicationUser> userManager, IOptions<AuthOptions> authOptions)
        {
            _accountService = new AccountService(userManager, authOptions);
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _accountService.Register(model);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);

            }
            return Ok();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _accountService.Login(model);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new { access_token = result.Token });
        }
    }
}