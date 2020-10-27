using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        IEnumerable<Task> GetForPage(int pageNumber, int pageSize, string userId, string role);
        void Create(Task value);
        void Delete(int id);
        string FindExetutorIdByEmail(string email);
        void Update(Task task);
        int GetTaskCount(string userId, string role);
    }
}
