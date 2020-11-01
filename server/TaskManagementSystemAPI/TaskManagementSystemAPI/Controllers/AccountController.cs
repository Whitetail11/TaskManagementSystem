using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataLayer.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystemAPI.Classes;

namespace TaskManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var result = await _accountService.Register(registerDTO);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);

            }
            return Ok(new { access_token = result.Token });
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _accountService.Login(loginDTO);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new { access_token = result.Token });
        }

        [Route("CreateUser")]
        [HttpPost]
        [Authorize(Roles = ApplicationConstants.Roles.ADMINISTRATOR)]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
        {
            var result = await _accountService.CreateUser(createUserDTO);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);

            }
            return Ok();
        }
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<GetUserDTO>> GetAllUsers()
        {
            var result = await _accountService.GetAllUsers();
            return result;
        }

        [Route("GetExecutorsForSelect")]
        [HttpGet]
        [Authorize(Roles = "administrator,customer")]
        public async Task<IActionResult> GetExecutorsForSelect()
        {
            var executors = await _accountService.GetUsersForSelect(ApplicationConstants.Roles.EXECUTOR);
            return Ok(executors);
        } 
    }
}
