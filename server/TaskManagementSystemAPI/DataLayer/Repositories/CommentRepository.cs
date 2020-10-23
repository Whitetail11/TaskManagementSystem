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

        public Comment Create(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            return comment;
        }

        public void Delete(int id)
        {
            _dbContext.Comments.AsNoTracking().Where(comment => comment.Id == id).Delete();
        }
    }
}
