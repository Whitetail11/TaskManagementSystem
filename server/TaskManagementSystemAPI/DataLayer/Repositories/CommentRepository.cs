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

        public void Create(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _dbContext.Comments.AsNoTracking().Where(comment => comment.Id == id).Delete();
            _dbContext.SaveChanges();
        }
    }
}
