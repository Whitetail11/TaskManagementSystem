using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Classes
{
    public class TaskFilter
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public string Title { get; set; }
        public int[] StatusIds { get; set; }
        public string ExecutorId { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
