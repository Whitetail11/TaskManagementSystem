using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Extension> Extensions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            //    Database.EnsureCreated();
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>(task =>
            {
                task.HasOne(t => t.User)
                    .WithMany(user => user.Tasks)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

                task.HasOne(t => t.Executor)
                    .WithMany(executor => executor.ExecutorTasks)
                    .HasForeignKey(t => t.ExecutorId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
