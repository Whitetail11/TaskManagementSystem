using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
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

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public IActionResult Post(CreateCommentDTO createCommentDTO)
        {
            var userId = User.Claims.FirstOrDefault(claim => claim.Type == "userid").Value;
            var showCommentDTO = _commentService.Create(createCommentDTO, userId);

            return Ok(showCommentDTO);
        }
    }
}
