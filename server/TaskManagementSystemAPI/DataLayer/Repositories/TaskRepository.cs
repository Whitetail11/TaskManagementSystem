using DataLayer.Classes;
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

        private IQueryable<Task> GetForAdmin()
        {
            return _dbContext.Tasks.AsNoTracking();
        }

        private IQueryable<Task> GetForCustomer(string creatorId)
        {
            return _dbContext.Tasks.AsNoTracking().Where(task => task.CreatorId == creatorId);
        }

        private IQueryable<Task> GetForExecutor(string executorId)
        {
            return _dbContext.Tasks.AsNoTracking().Where(task => task.ExecutorId == executorId);
        }

        private IQueryable<Task> GetFilteredTasks(TaskFilter taskFilter)
        {
            IQueryable<Task> tasks;
            switch (taskFilter.Role)
            {
                case ApplicationConstants.Roles.EXECUTOR:
                    tasks = GetForExecutor(taskFilter.UserId);
                    break;
                case ApplicationConstants.Roles.CUSTOMER:
                    tasks = GetForCustomer(taskFilter.UserId);
                    break;
                default:
                    tasks = GetForAdmin();
                    break;
            }

            if (taskFilter.Title != null)
            {
                tasks = tasks.Where(task => task.Title.Contains(taskFilter.Title));
            }

            if (taskFilter.StatusId != null)
            {
                var statusId = taskFilter.StatusId ?? 0;
                tasks = tasks.Where(task => task.StatusId == statusId);
            }

            if (taskFilter.ExecutorId != null)
            {
                tasks = tasks.Where(task => task.ExecutorId == taskFilter.ExecutorId);
            }

            if (taskFilter.Deadline != null)
            {
                var deadline = taskFilter.Deadline.Value;
                tasks = tasks.Where(task => task.Deadline <= deadline);
            }

            return tasks;
        }

        public IEnumerable<Task> GetForPage(TaskPage taskPage, TaskFilter taskFilter)
        {
            return GetFilteredTasks(taskFilter).OrderByDescending(task => task.Date)
                .Include(task => task.Executor)
                .Select(task => new Task() 
                { 
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description.Substring(0, ApplicationConstants.TASK_SHORT_INFO_CHARACTER_COUNT),
                    Deadline = task.Deadline,
                    StatusId = task.StatusId,
                    Executor = task.Executor,
                })
                .Skip((taskPage.PageNumber - 1) * taskPage.PageSize)
                .Take(taskPage.PageSize).ToList();
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
        public void ChangeStatus(int taskId, int statusId)
        {
            var res = _dbContext.Tasks.FirstOrDefault(t => t.Id == taskId);
            res.StatusId = statusId;
            this.Update(res);
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

        public int GetTaskCount(TaskFilter taskFilter)
        {
            return GetFilteredTasks(taskFilter).Count();
        }
    }
}
