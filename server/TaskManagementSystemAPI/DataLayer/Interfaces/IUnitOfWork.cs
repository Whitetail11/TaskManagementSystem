using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ITaskRepository TaskRepository { get; }
        ICommentRepository CommentRepository { get; }
        void Save();
    }
}
