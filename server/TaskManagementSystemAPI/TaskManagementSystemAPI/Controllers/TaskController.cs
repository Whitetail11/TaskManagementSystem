using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            _tasksService.CreateTask(task, email);
            return Ok();
        }
        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int id)
         {
            _tasksService.Delete(id);
            return Ok();
        }
    }
}
