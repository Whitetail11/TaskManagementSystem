using DataLayer.Interfaces;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITaskRepository TaskRepository { get; }
        public ICommentRepository CommentRepository { get; }

        public UnitOfWork(ApplicationContext dbContext)
        {
            TaskRepository = new TaskRepository(dbContext);
            CommentRepository = new CommentRepository(dbContext);
        }
    }
}
