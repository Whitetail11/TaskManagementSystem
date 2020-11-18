using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataLayer.Classes;
using DataLayer.Entities;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Services
{
    public class CommentService: ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IAccountService _accountService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly string _clientAppUrl;

        public CommentService(ICommentRepository commentRepository, ITaskRepository taskRepository, IAccountService accountService,
            INotificationService notificationService, IMapper mapper, IConfiguration configuration)
        {
            _commentRepository = commentRepository;
            _taskRepository = taskRepository;
            _accountService = accountService;
            _notificationService = notificationService;
            _mapper = mapper;
            _clientAppUrl = configuration.GetValue<string>("ClientAppUrl");
        }

        public void Create(CreateCommentDTO createCommentDTO, string userId, string role)
        {
            var comment = _mapper.Map<CreateCommentDTO, Comment>(createCommentDTO);
            comment.Date = DateTime.Now;
            comment.UserId = userId;
            _commentRepository.Create(comment);
            SendAfterCommentCreating(createCommentDTO.TaskId, userId, role);
        }

        public IEnumerable<ShowCommentDTO> GetForTask(int taskId)
        {
            var comments = _mapper.Map<IEnumerable<Comment>, IEnumerable<ShowCommentDTO>>(_commentRepository.GetForTask(taskId));
            return GroupComments(comments);
        }

        public void Delete(int id, int taskId)
        {
            var comments = _commentRepository.GetForTask(taskId);
            var commentToDeleteIds = GetChildIds(id, comments);
            commentToDeleteIds.Add(id);
            _commentRepository.Delete(commentToDeleteIds.ToArray());
        }

        public bool ExistAny(int id, string userId)
        {
            return _commentRepository.ExistAny(id, userId);
        }

        public IEnumerable<ShowCommentDTO> GroupComments(IEnumerable<ShowCommentDTO> comments)
        {
            var groupedComments = comments.ToList();
            foreach (var comment in comments)
            {
                if (comment.ReplyCommentId == null)
                {
                    continue;
                }

                groupedComments.Remove(comment);

                var parentComment = GetParentComment(comment.ReplyCommentId.Value, groupedComments);
                if (parentComment.Replies == null)
                {
                    parentComment.Replies = new List<ShowCommentDTO>();
                }
                parentComment.Replies.Add(comment);

                var replyComment = GetReplyComment(comment.ReplyCommentId.Value, groupedComments);
                if (replyComment.Id != parentComment.Id)
                {
                    comment.ReplyUserName = replyComment.UserName;
                }
            }
            return groupedComments;
        }

        private ShowCommentDTO GetReplyComment(int replyCommentId, IEnumerable<ShowCommentDTO> comments)
        {
            var replyComment = comments.FirstOrDefault(comment => comment.Id == replyCommentId);
            if (replyComment != null)
            {
                return replyComment;
            }
            return comments.Where(comment => comment.Replies != null)
                .SelectMany(comment => comment.Replies)
                .First(comment => comment.Id == replyCommentId);
        }

        private ShowCommentDTO GetParentComment(int replyCommentId, IEnumerable<ShowCommentDTO> comments)
        {
            return comments.First(comment => comment.Id == replyCommentId 
                || (comment.Replies != null && comment.Replies.Any(c => c.Id == replyCommentId)));
        }

        private List<int> GetChildIds(int id, IEnumerable<Comment> comments)
        {
            var allChildIds = new List<int>();

            var commentChildIds = comments
                .Where(comment => comment.ReplyCommentId == id)
                .Select(comment => comment.Id);

            allChildIds.AddRange(commentChildIds);

            foreach(var commentChildId in commentChildIds)
            {
                allChildIds.AddRange(GetChildIds(commentChildId, comments));
            }

            return allChildIds;
        }

        private async System.Threading.Tasks.Task SendAfterCommentCreating(int taskId, string userId, string role)
        {
            var email = string.Empty;
            if (role == ApplicationConstants.Roles.EXECUTOR)
            {
                email = _taskRepository.GetCreatorEmail(taskId);
            }
            else
            {
                email = _taskRepository.GetExecutorEmail(taskId);
            }

            if (await _accountService.IsEmailConfirmed(email))
            {
                var userFullName = _accountService.GetFullName(userId);
                var taskTitle = _taskRepository.GetTitle(taskId);
                var message = $"<b>{ userFullName }</b> has added comment to task <b>{ taskTitle }</b>. " +
                    $"To view the task and comments follow the <a href='{ _clientAppUrl }tasks/{ taskId }'>link</a>.";
                await _notificationService.SendEmailAsync(email, taskTitle, message);
            }
        } 
    }
}
