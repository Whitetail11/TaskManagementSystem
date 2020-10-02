//using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Migrations.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public ICollection<Task> TaskUser { get; set; }
        
        public ICollection<Task> TaskExecutor { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ErrorLog> Errors { get; set; }
        
    }
}
