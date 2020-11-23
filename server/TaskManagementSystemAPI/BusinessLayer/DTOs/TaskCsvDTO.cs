using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class TaskCSVDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public string Executor { get; set; }
        public string Creator { get; set; }
        public string Status { get; set; }
        public string Files { get; set; }
    }
}
