using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Mapping;
using BusinessLayer.Services;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace TaskManagementSystemAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IFileRepository, FileRepository>();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ITaskService, TaskService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Management System", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                      },
                      new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
