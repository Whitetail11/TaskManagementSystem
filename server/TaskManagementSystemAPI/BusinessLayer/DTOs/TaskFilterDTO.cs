using DataLayer.Classes;
using System;

namespace BusinessLayer.DTOs
{
    public class TaskFilterDTO
    {
        public string Title { get; set; }
        public int? StatusId { get; set; }
        public string ExecutorId { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
