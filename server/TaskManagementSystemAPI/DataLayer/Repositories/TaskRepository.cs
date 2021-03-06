﻿using DataLayer.Classes;
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

        public Task GetIncludedRelatedData(int id)
        {
            return _dbContext.Tasks.AsNoTracking()
                .Where(task => task.Id == id)
                .Include(task => task.Executor)
                .Include(task => task.Creator)
                .Include(task => task.Files)
                .Include(task => task.Comments)
                .ThenInclude(comment => comment.User)
                .FirstOrDefault();
        }

        public Task GetForExporting(int id)
        {
            return _dbContext.Tasks.AsNoTracking()
                .Where(task => task.Id == id)
                .Include(task => task.Executor)
                .Include(task => task.Creator)
                .Include(task => task.Status)
                .FirstOrDefault();
        }
    
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

            if (taskFilter.StatusIds != null && taskFilter.StatusIds.Length != 0)
            {
                tasks = tasks.Where(task => taskFilter.StatusIds.Contains(task.StatusId));
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

        public IEnumerable<Task> GetForPage(Page page, TaskFilter taskFilter)
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
                .Skip((page.Number - 1) * page.Size)
                .Take(page.Size).ToList();
        }

        public int Create(Task value)
        {
            var res = _dbContext.Tasks.Add(value).Entity;
            _dbContext.SaveChanges();
            return res.Id;
        }

        public void ChangeStatus(int taskId, int statusId)
        {
            var res = _dbContext.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == taskId);
            res.StatusId = statusId;
            this.Update(res);
        }
        public int Update(Task task)
        {
            var res = _dbContext.Tasks.Update(task).Entity;
            _dbContext.SaveChanges();
            return res.Id;
            
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

        public Task GetTaskById(int id)
        {
            var res = GetIncludedRelatedData(id);
            return res;
        }
        
        public bool HasUserAccess(int taskId, string userId)
        {
            return _dbContext.Tasks.AsNoTracking()
                .Any(task => task.Id == taskId && (task.ExecutorId == userId || task.CreatorId == userId));
        }

        public string GetExecutorId(int taskId)
        {
            return _dbContext.Tasks.AsNoTracking()
                .Where(task => task.Id == taskId)
                .Select(task => task.Executor.Id)
                .FirstOrDefault();
        }

        public string GetExecutorEmail(int taskId)
        {
            return _dbContext.Tasks.AsNoTracking()
                .Where(task => task.Id == taskId)
                .Select(task => task.Executor.Email)
                .FirstOrDefault();
        }

        public string GetCreatorEmail(int taskId)
        {
            return _dbContext.Tasks.AsNoTracking()
                .Where(task => task.Id == taskId)
                .Select(task => task.Creator.Email)
                .FirstOrDefault();
        }

        public string GetTitle(int taskId)
        {
            return _dbContext.Tasks.AsNoTracking()
                .Where(task => task.Id == taskId)
                .Select(task => task.Title)
                .FirstOrDefault();
        }
    }
}
