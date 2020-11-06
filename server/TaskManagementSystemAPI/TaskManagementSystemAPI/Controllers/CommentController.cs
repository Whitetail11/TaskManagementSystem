using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController: ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ITaskService _taskService;

        public CommentController(ICommentService commentService, ITaskService taskService)
        {
            _commentService = commentService;
            _taskService = taskService;
        }

        [HttpPost]
        public IActionResult Post(CreateCommentDTO createCommentDTO)
        {
            var userId = User.Claims.FirstOrDefault(claim => claim.Type == "userid").Value;
            _commentService.Create(createCommentDTO, userId);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetComments()
        {
            var comments = _taskService.GetComments(4);
            return Ok(_commentService.GroupComments(comments));
        }
    }
}
