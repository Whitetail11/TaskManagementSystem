using DataLayer.Classes;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        IEnumerable<Task> GetForPage(TaskPage taskPage, TaskFilter taskFilter);
        void Create(Task value);
        void Delete(int id);
        string FindExetutorIdByEmail(string email);
        string FindExecutorEmailById(string id);
        void Update(Task task);
        void ChangeStatus(int taskId, int statusId);
        Task GetTaskById(int id);
        int GetTaskCount(TaskFilter taskFilter);
        IEnumerable<Status> GetStatuses();
        IEnumerable<Comment> GetComments(int id);
        Task GetIncludedRelatedData(int id);
    }
}
