﻿using AutoMapper;
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
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files.Select(
                    el => new FileDTO { Id = el.Id, Name = el.Name, AttachedDate = el.AttachedDate }
                    )));
            CreateMap<TaskDTO, Task>()
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files.Select(
                    el => new File { Task = null, Id = el.Id, Name = el.Name, AttachedDate = el.AttachedDate }
                    )));

            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Task, ShowTaskShorInfoDTO>()
                .ForMember(
                    destinationMember => destinationMember.ExecutorName, 
                    memberOptions => memberOptions.MapFrom(task => $"{ task.Executor.Name } { task.Executor.Surname }"))
                .ForMember(
                    destinationMember => destinationMember.Deadline,
                    memberOptions => memberOptions.MapFrom(task => task.Deadline.ToShortDateString()))
                .ForMember(
                    destinationMember => destinationMember.MissedDeadline,
                    memberOptions => memberOptions.MapFrom(task => task.Deadline < DateTime.Now && task.StatusId != ApplicationConstants.DONE_STATUS_ID));

            CreateMap<Task, TaskCSVDTO>()
               .ForMember(
                   destinationMember => destinationMember.Executor,
                   memberOptions => memberOptions.MapFrom(task => $"{ task.Executor.Name } { task.Executor.Surname }"))
               .ForMember(
                   destinationMember => destinationMember.Creator,
                   memberOptions => memberOptions.MapFrom(task => $"{ task.Creator.Name } { task.Creator.Surname }"))
               .ForMember(
                   destinationMember => destinationMember.Deadline,
                   memberOptions => memberOptions.MapFrom(task => task.Deadline.ToShortDateString()))
               .ForMember(
                    destinationMember => destinationMember.Status,
                    memberOptions => memberOptions.MapFrom(task => task.Status.Name));

            CreateMap<Task, ShowTaskDTO>()
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files.Select(
                    el => new FileDTO { Id = el.Id, Name = el.Name, AttachedDate = el.AttachedDate }
                    )))
                .ForMember(
                    destinationMember => destinationMember.ExecutorName,
                    memberOptions => memberOptions.MapFrom(task => $"{ task.Executor.Name } { task.Executor.Surname }"))
                .ForMember(
                    destinationMember => destinationMember.CreatorName,
                    memberOptions => memberOptions.MapFrom(task => $"{ task.Creator.Name } { task.Creator.Surname }"))
                .ForMember(
                    destinationMember => destinationMember.Deadline,
                    memberOptions => memberOptions.MapFrom(task => task.Deadline.ToShortDateString()));
            CreateMap<Status, StatusDTO>();
            CreateMap<ApplicationUser, SelectUserDTO>()
                .ForMember(
                    destinationMember => destinationMember.FullName,
                    memberOptions => memberOptions.MapFrom(user => $"{ user.Name } { user.Surname }"));

            CreateMap<ApplicationUser, ShowListUserDTO>();

            CreateMap<ApplicationUser, ShowUserDTO>();
            CreateMap<Comment, ShowCommentDTO>()
                .ForMember(
                    destinationMember => destinationMember.UserName,
                    memberOptions => memberOptions.MapFrom(comment => $"{ comment.User.Name } { comment.User.Surname }"))
                .ForMember(
                    destinationMember => destinationMember.Date,
                    memberOptions => memberOptions.MapFrom(comment => comment.Date.ToShortDateString()));
            CreateMap<PageDTO, Page>();
            CreateMap<TaskFilterDTO, TaskFilter>();
        }
    }
}
