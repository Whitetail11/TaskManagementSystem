using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Mapping;
using BusinessLayer.Services;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManagementSystemAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICommentService, CommentService>();
            
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskservice, TaskService>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
