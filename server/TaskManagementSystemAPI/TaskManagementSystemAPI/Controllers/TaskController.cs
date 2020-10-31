﻿using BusinessLayer.DTOs;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public TaskController(ITaskService _tasksService)
        {
            this._tasksService = _tasksService;
        }

        [Route("GetForPage")]
        [HttpGet]
        public IActionResult GetForPage([FromQuery]TaskPageDTO taskPageDTO, [FromQuery]TaskFilterDTO taskFilterDTO)
        {
            var tasks = _tasksService.GetForPage(taskPageDTO, taskFilterDTO, HttpContext.GetUserId(), HttpContext.GetUserRole());
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult CreateTask(TaskDTO task, string email)
        {
            task.ExecutorId = _tasksService.FindExecutorIdByEmail(email);
            if(task.ExecutorId == null)
            {
                return BadRequest(new { message = "Not found email of executor." });
            }
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
