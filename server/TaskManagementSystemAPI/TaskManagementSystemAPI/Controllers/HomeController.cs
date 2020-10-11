using DataLayer;
using DataLayer.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq;

namespace TaskManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public string Get()
        {
            return "It's home controller";
        }

        [Route("GetUserInfo")]
        [HttpGet]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.Users.AsNoTracking().FirstOrDefault(u => u.Id == userId);

            var response = new
            {
                email = user.Email,
                name = user.Name,
                surname = user.Surname,
            };

            return Ok(response);
        }

        [Route("GetExecutorInfo")]
        [HttpGet]
        [Authorize(Roles = "executor")]
        public IActionResult GetExecutorInfo()
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.Users.AsNoTracking().FirstOrDefault(u => u.Id == userId);

            var response = new
            {
                email = user.Email,
                name = user.Name,
                surname = user.Surname,
            };

            return Ok(response);
        }
    }
}
