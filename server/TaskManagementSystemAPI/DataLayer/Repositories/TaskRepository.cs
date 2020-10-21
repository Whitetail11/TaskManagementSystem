using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public class TaskRepository: Repository<Task>, ITaskRepository
    {
        public TaskRepository(ApplicationContext context)
            : base(context)
        { }
        public List<Task> GetAllTasks()
        {
            return _dbContext.Tasks
                .Include(n => n.Executor)
                .Include(m => m.Creator)
                .Include(n => n.Status)
                .Include(n => n.Comments)
                .Include(n => n.Files)
                .ToList();
        }
        public void Create(Task value)
        {
            _dbContext.Tasks.Add(value);
            _dbContext.SaveChanges();
        }
        public string FindExetutorIdByEmail(string email)
        {
            var res = _dbContext.Users.FirstOrDefault(m => m.Email == email);
            if (res == null)
                return null;
            return res.Id;
        }
        public void Update(Task task)
        {
            _dbContext.Tasks.Update(task);
            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var task = _dbContext.Tasks.FirstOrDefault(m => m.Id == id);
            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();
        }
    }
}
