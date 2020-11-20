﻿using DataLayer.Classes;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        IEnumerable<Task> GetForPage(Page page, TaskFilter taskFilter);
        void Create(Task value);
        void Delete(int id);
        void Update(Task task);
        void ChangeStatus(int taskId, int statusId);
        Task GetTaskById(int id);
        int GetTaskCount(TaskFilter taskFilter);
        Task GetIncludedRelatedData(int id);
        bool HasUserAccess(int taskId, string userId);
        string GetExecutorEmail(int taskId);
        string GetCreatorEmail(int taskId);
        string GetTitle(int taskId);
    }
}
