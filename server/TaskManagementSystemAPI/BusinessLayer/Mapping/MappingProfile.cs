using AutoMapper;
using BusinessLayer.DTOs;
using DataLayer.Classes;
using DataLayer.Entities;
using DataLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Task, TaskDTO>()
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(
                    el => new ShowCommentDTO { Date = el.Date, Id = el.Id, Text = el.Text, UserId = el.UserId }
                    )))
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files.Select(
                    el => new FileDTO { Task = null, Id = el.Id, Data = el.Data, Name = el.Name, AttachedDate = el.AttachedDate }
                    )));
            CreateMap<TaskDTO, Task>()
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(
                    el => new Comment { Date = el.Date, Id = el.Id, Task = null, Text = el.Text, User = null }
                    )))
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files.Select(
                    el => new File { Task = null, Id = el.Id, Data = el.Data, Name = el.Name, AttachedDate = el.AttachedDate }
                    )));

            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Task, ShowTaskShorInfoDTO>()
                .ForMember(
                    destinationMember => destinationMember.ExecutorName, 
                    memberOptions => memberOptions.MapFrom(task => $"{ task.Executor.Name } { task.Executor.Surname }"))
                .ForMember(
                    destinationMember => destinationMember.Deadline,
                    memberOptions => memberOptions.MapFrom(task => $"{ task.Deadline.ToShortDateString() }"));
            CreateMap<Task, ShowTaskDTO>()
                .ForMember(
                    destinationMember => destinationMember.ExecutorName,
                    memberOptions => memberOptions.MapFrom(task => $"{ task.Executor.Name } { task.Executor.Surname }"))
                .ForMember(
                    destinationMember => destinationMember.CreatorName,
                    memberOptions => memberOptions.MapFrom(task => $"{ task.Creator.Name } { task.Creator.Surname }"));
            CreateMap<Status, StatusDTO>();
            CreateMap<ApplicationUser, SelectUserDTO>()
                .ForMember(
                    destinationMember => destinationMember.FullName,
                    memberOptions => memberOptions.MapFrom(user => $"{ user.Name } { user.Surname }"));
            CreateMap<Comment, ShowCommentDTO>()
                .ForMember(
                    destinationMember => destinationMember.UserName,
                    memberOptions => memberOptions.MapFrom(comment => $"{ comment.User.Name } { comment.User.Surname }"));
            CreateMap<TaskPageDTO, TaskPage>();
            CreateMap<TaskFilterDTO, TaskFilter>();

        }
    }
}
