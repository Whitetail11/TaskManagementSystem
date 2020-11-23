using DataLayer.Classes;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        IEnumerable<Task> GetForPage(Page page, TaskFilter taskFilter);
        Task GetForExporting(int id);
        int Create(Task value);
        void Delete(int id);
        int Update(Task task);
        void ChangeStatus(int taskId, int statusId);
        Task GetTaskById(int id);
        int GetTaskCount(TaskFilter taskFilter);
        Task GetIncludedRelatedData(int id);
        bool HasUserAccess(int taskId, string userId);
        string GetExecutorId(int taskId);
        string GetExecutorEmail(int taskId);
        string GetCreatorEmail(int taskId);
        string GetTitle(int taskId);
    }
}
