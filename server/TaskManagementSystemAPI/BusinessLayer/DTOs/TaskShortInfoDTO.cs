using System;

namespace BusinessLayer.DTOs
{
    public class TaskShortInfoDTO
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UserShortInfoDTO Executor { get; set; }
        public StatusDTO Status { get; set; }
    }
}
