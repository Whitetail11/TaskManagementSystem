using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TaskManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationContext _dbContext;
        public HomeController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
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
            var userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            var user = _dbContext.ApplicationUsers.AsNoTracking().FirstOrDefault(u => u.Id == userId);

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
            var userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            var user = _dbContext.ApplicationUsers.AsNoTracking().FirstOrDefault(u => u.Id == userId);

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
