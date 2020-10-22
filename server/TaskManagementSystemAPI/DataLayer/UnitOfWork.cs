using DataLayer.Interfaces;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _dbContext;
        public ITaskRepository TaskRepository { get; }
        public ICommentRepository CommentRepository { get; }

        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            TaskRepository = new TaskRepository(dbContext);
            CommentRepository = new CommentRepository(dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
