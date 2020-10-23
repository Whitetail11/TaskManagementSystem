using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "New" },
                new Status { Id = 2, Name = "In progress" },
                new Status { Id = 3, Name = "On hold" },
                new Status { Id = 4, Name = "Done" }
                );
        }
    }
}
