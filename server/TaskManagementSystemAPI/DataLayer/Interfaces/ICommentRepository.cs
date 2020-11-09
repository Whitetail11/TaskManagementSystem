using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interfaces
{
    public interface ICommentRepository: IRepository<Comment>
    {
        IEnumerable<Comment> GetForTask(int taskId);
        void Create(Comment comment);
        void Delete(params int[] ids);
        bool ExistAny(int id, string userId);
    }
}
