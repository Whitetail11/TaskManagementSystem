using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interfaces
{
    public interface IUnitOfWork
    {
        ITaskRepository TaskRepository { get; }
        ICommentRepository CommentRepository { get; }
    }
}
