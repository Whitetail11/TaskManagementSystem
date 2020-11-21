using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataLayer.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetUserById(string id)
        {
            if (id == null || !(await _accountService.ExistAnyUserWithId(id))
                || (id != HttpContext.GetUserId() && HttpContext.GetUserRole() != ApplicationConstants.Roles.ADMINISTRATOR))
            {
                return NotFound();
            }

            var user = await _accountService.GetUserById(id);
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = ApplicationConstants.Roles.ADMINISTRATOR)]
        public async Task<IActionResult> GetForPage([FromQuery]PageDTO pageDTO)
        {
            var users = await _accountService.GetForPage(pageDTO);
            return Ok(users);
        }

        [Route("GetUserCount")]
        [HttpGet]
        [Authorize(Roles = ApplicationConstants.Roles.ADMINISTRATOR)]
        public async Task<IActionResult> GetUserCount()
        {
            var count = await _accountService.GetUserCount();
            return Ok(count);
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

        [Route("SendEmailConfirmationLink")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SendEmailConfirmationLink()
        {
            await _accountService.SendEmailConfirmationLink(HttpContext.GetUserId());
            return Ok();
        }

        [Route("ConfirmEmail")]
        [HttpPut]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDTO confirmEmailDTO)
        {
            var result = await _accountService.ConfirmEmail(confirmEmailDTO);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [Route("ForgotPassword")]
        [HttpGet]
        public async Task<IActionResult> ForgotPassword([FromQuery]ForgotPasswordDTO forgotPasswordDTO)
        {
            await _accountService.ForgotPassword(forgotPasswordDTO);
            return Ok();
        } 

        [Route("ResetPassword")]
        [HttpPut]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var result = await _accountService.ResetPassword(resetPasswordDTO);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        } 

        [Route("ChangePassword")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var result = await _accountService.ChangePassword(HttpContext.GetUserId(), changePasswordDTO);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

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

        [Route("{id}")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, [FromBody]UpdateUserDTO updateUserDTO)
        {
            if (id == null || !(await _accountService.ExistAnyUserWithId(id))
                || (id != HttpContext.GetUserId() && HttpContext.GetUserRole() != ApplicationConstants.Roles.ADMINISTRATOR))
            {
                return NotFound();
            }

            var result = await _accountService.UpdateUser(id, updateUserDTO);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }
      
        [Route("GetExecutorsForSelect")]
        [HttpGet]
        [Authorize(Roles = ApplicationConstants.Roles.ADMINISTRATOR + "," + ApplicationConstants.Roles.CUSTOMER)]
        public async Task<IActionResult> GetExecutorsForSelect()
        {
            var executors = await _accountService.GetUsersForSelect(ApplicationConstants.Roles.EXECUTOR);
            return Ok(executors);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var id = HttpContext.GetUserId();
            if (!await _accountService.ExistAnyUserWithId(id))
            {
                return NotFound();
            }

            await _accountService.DeleteUser(id);
            return Ok();
        }
    }
}
