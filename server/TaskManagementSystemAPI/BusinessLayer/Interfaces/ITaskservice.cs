﻿using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskShortInfoDTO> GetForPage(TaskPageDTO taskPageDTO, TaskFilterDTO taskFilterDTO, string userId, string role);
        void CreateTask(TaskDTO task);
        void Delete(int id);
        string FindExecutorIdByEmail(string email);
        void Update(TaskDTO task);
        void ChangeStatus(int taskId, int statusId);
        int GetTaskCount(TaskFilterDTO taskFilterDTO, string userId, string role);
        int GetPageCount(int pageSize, TaskFilterDTO taskPageDTO, string userId, string role);
    }
}
