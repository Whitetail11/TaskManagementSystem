using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataLayer.Classes;
using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICommentService _commentService;
        private readonly INotificationService _notificationService;
        private readonly IStatusService _statusService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly string _clientAppUrl;

        public TaskService(ITaskRepository taskRepository, ICommentService commentService, IMapper mapper,
            INotificationService notificationService, IConfiguration configuration, IStatusService statusService,
            IAccountService accountService)
        {
            _taskRepository = taskRepository;
            _commentService = commentService;
            _notificationService = notificationService;
            _statusService = statusService;
            _accountService = accountService;
            _mapper = mapper;
            _clientAppUrl = configuration.GetValue<string>("ClientAppUrl");
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
            SendEmailAfterCreating(task);
        }
        public void ChangeStatus(int taskId, int statusId)
        {
            _taskRepository.ChangeStatus(taskId, statusId);
            SendEmailAfterStatusChanging(taskId, statusId);
        }

        public void Delete(int id)
        {
            _taskRepository.Delete(id);
        }

        public void Update(TaskDTO task)
        {
            task.StatusId = 1;
            var res = _mapper.Map<TaskDTO, Task>(task);
            _taskRepository.Update(res);
            SendEmailAfterUpdating(res);
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

        public bool HasUserAccess(int taskId, string userId)
        {
            return _taskRepository.HasUserAccess(taskId, userId);
        }

        public string GetExecutorEmail(int taskId)
        {
            return _taskRepository.GetExecutorEmail(taskId);
        }

        public string GetCreatorEmail(int taskId)
        {
            return _taskRepository.GetCreatorEmail(taskId);
        }

        public string GetTitle(int taskId)
        {
            return _taskRepository.GetTitle(taskId);
        }

        private async System.Threading.Tasks.Task SendEmailAfterCreating(DataLayer.Entities.Task task)
        {
            var executorEmail = GetExecutorEmail(task.Id);
            if (await _accountService.IsEmailConfirmed(executorEmail))
            {
                var message = $"Task <b>{ task.Title }</b> has been assigned to you. " +
                $"To view the task follow the <a href='{ _clientAppUrl }tasks/{ task.Id }'>link</a>.";
                await _notificationService.SendEmailAsync(executorEmail, task.Title, message);
            }
        }

        private async System.Threading.Tasks.Task SendEmailAfterUpdating(DataLayer.Entities.Task task)
        {
            var executorEmail = GetExecutorEmail(task.Id);
            if (await _accountService.IsEmailConfirmed(executorEmail))
            {
                var message = $"Task <b>{ task.Title }</b> has been updated. " +
                $"To view the task follow the <a href='{ _clientAppUrl }tasks/{ task.Id }'>link</a>.";
                await _notificationService.SendEmailAsync(executorEmail, task.Title, message);
            }
        }

        private async System.Threading.Tasks.Task SendEmailAfterStatusChanging(int taskId, int statusId)
        {
            var creatorEmail = GetCreatorEmail(taskId);
            if (await _accountService.IsEmailConfirmed(creatorEmail))
            {
                var taskTitle = GetTitle(taskId);
                var statusName = _statusService.GetName(statusId);
                var message = $"Status of task <b>{ taskTitle }</b> has been changed to <b>{statusName}</b>. " +
                    $"To view the task follow the <a href='{ _clientAppUrl }tasks/{ taskId }'>link</a>.";
                await _notificationService.SendEmailAsync(creatorEmail, taskTitle, message);
            }
        }
    }
}
