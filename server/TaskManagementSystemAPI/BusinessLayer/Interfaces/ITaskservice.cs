using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public interface ITaskService
    {
        IEnumerable<ShowTaskShorInfoDTO> GetForPage(PageDTO pageDTO, TaskFilterDTO taskFilterDTO, string userId, string role);
        void CreateTask(TaskDTO task);
        void Delete(int id);
        void Update(TaskDTO task);
        void ChangeStatus(int taskId, int statusId);
        TaskDTO GetTaskById(int id);
        int GetTaskCount(TaskFilterDTO taskFilterDTO, string userId, string role);
        int GetPageCount(int pageSize, TaskFilterDTO taskPageDTO, string userId, string role);
        ShowTaskDTO GetForShowing(int id);
        bool HasUserAccess(int taskId, string userId);
        string GetExecutorEmail(int taskId);
        string GetCreatorEmail(int taskId);
        string GetTitle(int taskId);
    }
}
