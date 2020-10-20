using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        List<Task> GetAllTasks();
        void Create(Task value);
        void Delete(int id);
    }
}
