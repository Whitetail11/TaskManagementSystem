using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataLayer.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Services
{
    public class CommentService: ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public void Create(CreateCommentDTO createCommentDTO, string userId)
        {
            var comment = _mapper.Map<CreateCommentDTO, Comment>(createCommentDTO);
            comment.Date = DateTime.Now;
            comment.UserId = userId;
            _commentRepository.Create(comment);
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
    }
}
