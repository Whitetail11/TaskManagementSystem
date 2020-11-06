using DataLayer.Classes;
using DataLayer.Entities;
using DataLayer.Extensions;
using DataLayer.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Extension> Extensions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
            
            DbInitializer.Initialize(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Task>(task =>
            {
                task.HasOne(t => t.Creator)
                    .WithMany(user => user.Tasks)
                    .HasForeignKey(t => t.CreatorId)
                    .OnDelete(DeleteBehavior.NoAction);

                task.HasOne(t => t.Executor)
                    .WithMany(executor => executor.ExecutorTasks)
                    .HasForeignKey(t => t.ExecutorId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Seed();
            });
        }
    }
}
