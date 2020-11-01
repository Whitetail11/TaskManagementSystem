using DataLayer.Classes;
using System;
using System.Collections.Generic;

namespace BusinessLayer.DTOs
{
    public class TaskFilterDTO
    {
        public string Title { get; set; }
        public int[] StatusIds { get; set; }
        public string ExecutorId { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
