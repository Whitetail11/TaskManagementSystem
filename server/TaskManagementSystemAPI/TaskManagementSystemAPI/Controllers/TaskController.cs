using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace TaskManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskservice _tasksService;
        public TaskController(ITaskservice _tasksService)
        {
            this._tasksService = _tasksService;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetTasks()
        {
            List<TaskDTO> objectList = _tasksService.GetTasks();
            return Ok(objectList);
        }
        [HttpPost]
        [Authorize]
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
        [Authorize]
        public IActionResult Delete(int id)
         {
            _tasksService.Delete(id);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public IActionResult Update(TaskDTO task)
        {
            _tasksService.Update(task);
            return Ok();
        }
    }
}
