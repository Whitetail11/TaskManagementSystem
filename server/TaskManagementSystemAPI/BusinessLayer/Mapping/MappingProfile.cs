using AutoMapper;
using BusinessLayer.DTOs;
using DataLayer.Entities;
using DataLayer.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<ErrorLog, ErrorLogDTO>();
            CreateMap<File, FileDTO>();
            CreateMap<Status, StatusDTO>();
            CreateMap<Task, TaskDTO>();
        }
    }
}
