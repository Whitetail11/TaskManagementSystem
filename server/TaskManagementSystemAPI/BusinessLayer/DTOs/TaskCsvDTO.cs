using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class TaskCsvDTO
    {
        public DateTime Deadline { get; set; }
        public string ExecutorEmail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<string> FileNames { get; set; }
    }
}
