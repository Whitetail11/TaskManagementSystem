﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Task
    {
        public int Id {get; set;}
        public DateTime Deadline { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ExecutorId { get; set; }
        public int StatusId { get; set; }
        
        public virtual User User { get; set; }
        public virtual User Executor { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
