using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interfaces
{
    public interface ICommentRepository: IRepository<Comment>
    {
        void Create(Comment comment);
        void Delete(int id);
    }
}
