using BusinessLayer.DTOs;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetForPage([FromQuery]int pageNumber, [FromQuery]int pageSize)
        {
            var taskPageDTO = new TaskPageDTO(pageNumber, pageSize, HttpContext.GetUserId(), HttpContext.GetUserRole());
            var tasks = _tasksService.GetForPage(taskPageDTO);
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
        
        [HttpPut]
        public IActionResult Update(TaskDTO task)
        {
            _tasksService.Update(task);
            return Ok();
        }

        [Route("GetPageCount")]
        [HttpGet]
        public IActionResult GetPageCount(int pageSize)
        {
            var taskPageDTO = new TaskPageDTO(pageSize, HttpContext.GetUserId(), HttpContext.GetUserRole());
            var count = _tasksService.GetPageCount(taskPageDTO);
            return Ok(count);
        } 
    }
}
