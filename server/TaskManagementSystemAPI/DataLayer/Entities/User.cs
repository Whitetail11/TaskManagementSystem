using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
       
        public virtual Role Role { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Task> ExecutorTasks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ErrorLog> Errors { get; set; }
        
    }
}
