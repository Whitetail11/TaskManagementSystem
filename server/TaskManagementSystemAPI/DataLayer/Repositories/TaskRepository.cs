using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    public class TaskRepository: Repository<Task>, ITaskRepository
    {
        public TaskRepository(ApplicationContext context)
            : base(context)
        { }

        public IEnumerable<Task> GetForPage(int pageNumber, int pageSize)
        {
            var tasks = _dbContext.Tasks
                .OrderByDescending(task => task.Date)
                .Include(task => task.Executor)
                .Include(task => task.Status);

            return tasks.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
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

        public int GetTaskCount()
        {
            return _dbContext.Tasks.AsNoTracking().Count();
        }
    }
}
