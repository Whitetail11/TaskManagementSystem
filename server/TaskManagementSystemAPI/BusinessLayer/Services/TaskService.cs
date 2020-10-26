using AutoMapper;
using BusinessLayer.DTOs;
using DataLayer.Entities;
using DataLayer.Repositories;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository _taskRepository, IMapper _mapper)
        {
            this._taskRepository = _taskRepository;
            this._mapper = _mapper;
        }

        public IEnumerable<TaskShortInfoDTO> GetForPage(TaskPageDTO taskPageDTO)
        {
            var tasks = _taskRepository.GetForPage(taskPageDTO.PageNumber, taskPageDTO.PageSize);
            return _mapper.Map<IEnumerable<TaskShortInfoDTO>>(tasks);
        }

        public void CreateTask(TaskDTO taskdto)
        {
            taskdto.StatusId = 1;
            Task task = _mapper.Map<TaskDTO, Task>(taskdto);
            _taskRepository.Create(task);
        }

        public void Delete(int id)
        {
            _taskRepository.Delete(id);
        }

        public string FindExecutorIdByEmail(string email)
        {
            var res = _taskRepository.FindExetutorIdByEmail(email);
            return res;
        }

        public void Update(TaskDTO task)
        {
            var res = _mapper.Map<TaskDTO, Task>(task);
            _taskRepository.Update(res);
        }

        public int GetPageCount(int pageSize)
        {
            if(pageSize < 1)
            {
                return 1;
            }

            return (_taskRepository.GetTaskCount() + pageSize - 1) / pageSize;
        }
    }
}
