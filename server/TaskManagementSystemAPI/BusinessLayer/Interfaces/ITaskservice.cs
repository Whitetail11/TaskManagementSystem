using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public interface ITaskservice
    {
        List<TaskDTO> GetTasks();
        void CreateTask(TaskDTO task);
        void Delete(int id);
        string FindExecutorIdByEmail(string email);
        void Update(TaskDTO task);
    }
}
