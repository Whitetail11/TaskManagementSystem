using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Migrations.Models
{
    public class Task
    {
        public int Id {get; set;}
        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Deadline { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("Executor")]
        public int ExecutorId { get; set; }
        [ForeignKey("ExecutorId")]
        public User Executor { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        
        public Status Status { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<File> Files { get; set; }
    }
}
