using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataLayer.Classes;
using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, ICommentService commentService, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _commentService = commentService;
            _mapper = mapper;
        }

        public IEnumerable<ShowTaskShorInfoDTO> GetForPage(TaskPageDTO taskPageDTO, TaskFilterDTO taskFilterDTO, string userId, string role)
        {
            var taskPage = _mapper.Map<TaskPageDTO, TaskPage>(taskPageDTO);
            var taskFilter = _mapper.Map<TaskFilterDTO, TaskFilter>(taskFilterDTO);
            taskFilter.UserId = userId;
            taskFilter.Role = role;
            
            var tasks = _taskRepository.GetForPage(taskPage, taskFilter);
            return _mapper.Map<IEnumerable<ShowTaskShorInfoDTO>>(tasks);
        }

        public ShowTaskDTO GetForShowing(int id)
        {
            var task = _mapper.Map<Task, ShowTaskDTO>(_taskRepository.GetIncludedRelatedData(id));
            if (task != null)
            {
                task.Comments = _commentService.GroupComments(task.Comments);
            }
            return task;
        }

        public TaskDTO GetTaskById(int id)
        {
            var task = _taskRepository.GetTaskById(id);
            return _mapper.Map<TaskDTO>(task);
        }

        public void CreateTask(TaskDTO taskdto)
        {
            taskdto.StatusId = 1;
            Task task = _mapper.Map<TaskDTO, Task>(taskdto);
            _taskRepository.Create(task);
        }
        public void ChangeStatus(int taskId, int statusId)
        {
            _taskRepository.ChangeStatus(taskId, statusId);
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
        public string FindExecutorEmailById(string id)
        {
            var res = _taskRepository.FindExecutorEmailById(id);
            return res;
        }

        public void Update(TaskDTO task)
        {
            task.StatusId = 1;
            var res = _mapper.Map<TaskDTO, Task>(task);
            _taskRepository.Update(res);
        }

        public int GetTaskCount(TaskFilterDTO taskFilterDTO, string userId, string role)
        {
            var taskFilter = _mapper.Map<TaskFilterDTO, TaskFilter>(taskFilterDTO);
            taskFilter.UserId = userId;
            taskFilter.Role = role;
            return _taskRepository.GetTaskCount(taskFilter);
        }

        public int GetPageCount(int pageSize, TaskFilterDTO taskPageDTO, string userId, string role)
        {
            if (pageSize < 1)
            {
                pageSize = ApplicationConstants.DEFAULT_TASK_PAGE_SIZE;
            }
            var taskCount = GetTaskCount(taskPageDTO, userId, role);

            return (taskCount + pageSize - 1) / pageSize;
        }

        public IEnumerable<StatusDTO> GetStatuses()
        {
            var statuses = _taskRepository.GetStatuses();
            return _mapper.Map<IEnumerable<StatusDTO>>(statuses);
        }
    }
}
