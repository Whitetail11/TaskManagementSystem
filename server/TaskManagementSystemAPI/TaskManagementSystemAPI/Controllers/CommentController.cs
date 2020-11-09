using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystemAPI.Extensions;

namespace TaskManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController: ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Route("GetForTask/{taskId}")]
        [HttpGet]
        public IActionResult GetForTask(int taskId)
        {
            var comments = _commentService.GetForTask(taskId);
            return Ok(comments);
        }

        [HttpPost]
        public IActionResult Post(CreateCommentDTO createCommentDTO)
        {
            var userId = User.Claims.FirstOrDefault(claim => claim.Type == "userid").Value;
            _commentService.Create(createCommentDTO, userId);
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id, [FromQuery]int taskId)
        {
            if (!_commentService.ExistAny(id, HttpContext.GetUserId()))
            {
                return NotFound();
            }

            _commentService.Delete(id, taskId);

            return Ok();
        }
    }
}
