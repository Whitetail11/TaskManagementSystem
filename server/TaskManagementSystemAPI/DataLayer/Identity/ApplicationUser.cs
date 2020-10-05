using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Identity
{
    public class ApplicationUser: IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Surname { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Task> ExecutorTasks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ErrorLog> Errors { get; set; }
    }
}
