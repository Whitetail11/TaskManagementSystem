using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataLayer.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using TaskManagementSystemAPI.Extensions;

namespace TaskManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _tasksService;
        private readonly INotificationService _notificationService;
        private readonly IAccountService _accountService;

        public TaskController(ITaskService _tasksService, INotificationService _notificationService, IAccountService accountService)
        {
            this._tasksService = _tasksService;
            this._accountService = accountService;
            this._notificationService = _notificationService;
        }

        [Route("GetForPage")]
        [HttpGet]
        public IActionResult GetForPage([FromQuery]PageDTO pageDTO, [FromQuery]TaskFilterDTO taskFilterDTO)
        {
            var tasks = _tasksService.GetForPage(pageDTO, taskFilterDTO, HttpContext.GetUserId(), HttpContext.GetUserRole());
            return Ok(tasks);
        }

        [Route("GetForShowing/{id}")]
        [HttpGet]
        public IActionResult GetForShowing([FromRoute]int id)
        {
            if (HttpContext.GetUserRole() != ApplicationConstants.Roles.ADMINISTRATOR
                && !_tasksService.HasUserAccess(id, HttpContext.GetUserId()))
            {
                return NotFound();
            }

            var task = _tasksService.GetForShowing(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpGet]
        public IActionResult GetTaskById(int id)
        {
            var res = _tasksService.GetTaskById(id);
            return Ok(res);
        }
        [HttpPost]
        public IActionResult CreateTask(TaskDTO task)
        {
            _tasksService.CreateTask(task);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
         {
            _tasksService.Delete(id);
            return Ok();
        }
        [Route("ChangeStatus")]
        [HttpPut]
        public IActionResult ChangeStatus(int taskId, int statusId)
        {
            _tasksService.ChangeStatus(taskId, statusId);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(TaskDTO task)
        {           
            _tasksService.Update(task);
            return Ok();
        }

        [Route("GetTaskCount")]
        [HttpGet]
        public IActionResult GetTaskCount([FromQuery]TaskFilterDTO taskFilterDTO)
        {
            var count = _tasksService.GetTaskCount(taskFilterDTO, HttpContext.GetUserId(), HttpContext.GetUserRole());
            return Ok(count);
        }

        [Route("GetPageCount")]
        [HttpGet]
        public IActionResult GetPageCount([FromQuery]int pageSize, [FromQuery]TaskFilterDTO taskFilterDTO)
        {
            var count = _tasksService.GetPageCount(pageSize, taskFilterDTO, HttpContext.GetUserId(), HttpContext.GetUserRole());
            return Ok(count);
        } 
    }
}
