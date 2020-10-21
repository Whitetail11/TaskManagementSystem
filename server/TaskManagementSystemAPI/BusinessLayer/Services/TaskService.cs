using AutoMapper;
using BusinessLayer.DTOs;
using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Services
{
    public class TaskService : ITaskservice
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository _taskRepository, IMapper _mapper)
        {
            this._taskRepository = _taskRepository;
            this._mapper = _mapper;
        }

        public void CreateTask(TaskDTO taskdto, string email)
        {
            taskdto.ExecutorId = this.FindExecutorIdByEmail(email);
            taskdto.StatusId = 1;
            Task task = _mapper.Map<TaskDTO, Task>(taskdto);
            _taskRepository.Create(task);
        }

        public List<TaskDTO> GetTasks()
        {
            var tasks = _taskRepository.GetAllTasks();
            return _mapper.Map<List<TaskDTO>>(tasks.ToList());
        }
        public void Delete(int id)
        {
            this._taskRepository.Delete(id);
        }
        public string FindExecutorIdByEmail(string email)
        {
            var res = _taskRepository.FindExetutorIdByEmail(email);
            return res;
        }

    }
}
