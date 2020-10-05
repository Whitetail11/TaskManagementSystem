using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

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
            _dbContext.Statuses.Add(new Status() { Name = "NEW"});
            _dbContext.SaveChanges();

            return "Hello, world";
        }
    }
}