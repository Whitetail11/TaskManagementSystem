using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 2, Name = "New" },
                new Status { Id = 3, Name = "In progress" },
                new Status { Id = 4, Name = "On hold" },
                new Status { Id = 5, Name = "Done" }
                );
        }
    }
}
