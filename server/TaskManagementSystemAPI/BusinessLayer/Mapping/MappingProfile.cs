using AutoMapper;
using BusinessLayer.DTOs;
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
            CreateMap<Comment, ShowCommentDTO>();
        }
    }
}
