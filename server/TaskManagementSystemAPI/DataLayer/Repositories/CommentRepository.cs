using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Z.EntityFramework.Plus;

namespace DataLayer.Repositories
{
    public class CommentRepository: Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationContext dbContext): base(dbContext)
        {}

        public IEnumerable<Comment> GetForTask(int taskId)
        {
            return _dbContext.Comments.AsNoTracking()
                .Where(comment => comment.TaskId == taskId)
                .Include(comment => comment.User)
                .ToList();
        }

        public void Create(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
        }

        public void Delete(params int[] ids)
        {
            _dbContext.Comments.AsNoTracking().Where(comment => ids.Contains(comment.Id)).Delete();
            _dbContext.SaveChanges();
        }

        public bool ExistAny(int id, string userId)
        {
            return _dbContext.Comments.Any(comment => comment.Id == id && comment.UserId == userId);
        }
    }
}
