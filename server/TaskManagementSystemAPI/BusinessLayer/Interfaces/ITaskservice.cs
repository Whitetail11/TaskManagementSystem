﻿using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public interface ITaskService
    {
        IEnumerable<ShowTaskShorInfoDTO> GetForPage(TaskPageDTO taskPageDTO, TaskFilterDTO taskFilterDTO, string userId, string role);
        int CreateTask(TaskDTO task);
        void Delete(int id);
        string FindExecutorIdByEmail(string email);
        string FindExecutorEmailById(string id);
        int Update(TaskDTO task);
        void ChangeStatus(int taskId, int statusId);
        TaskDTO GetTaskById(int id);
        int GetTaskCount(TaskFilterDTO taskFilterDTO, string userId, string role);
        int GetPageCount(int pageSize, TaskFilterDTO taskPageDTO, string userId, string role);
        ShowTaskDTO GetForShowing(int id);
        bool HasUserAccess(int taskId, string userId);
    }
}
