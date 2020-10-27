using System;

namespace BusinessLayer.DTOs
{
    public class TaskShortInfoDTO
    {
        public int Id { get; set; }
        public string Deadline { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExecutorName { get; set; }
        public int StatusId { get; set; }
    }
}
