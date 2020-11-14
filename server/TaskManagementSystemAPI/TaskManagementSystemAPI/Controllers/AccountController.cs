using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataLayer.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystemAPI.Classes;
using TaskManagementSystemAPI.Extensions;

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

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            if (id != null && id != HttpContext.GetUserId())
            {
                return NotFound();
            }

            var user = await _accountService.Get(HttpContext.GetUserId());
            return Ok(user);
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

        [Route("ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string userId, [FromQuery]string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest(new List<string>() { "Email address confirmation link is invalid." });
            }

            var result = await _accountService.ConfirmEmail(userId, code);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [Route("SendEmailConfirmationLink")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SendEmailConfirmationLink()
        {
            await _accountService.SendEmailConfirmationLink(HttpContext.GetUserId());
            return Ok();
        }

        [Route("ForgotPassword")]
        [HttpGet]
        public async Task<IActionResult> ForgotPassword([FromQuery]ForgotPasswordDTO forgotPasswordDTO)
        {
            await _accountService.ForgotPassword(forgotPasswordDTO);
            return Ok();
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
