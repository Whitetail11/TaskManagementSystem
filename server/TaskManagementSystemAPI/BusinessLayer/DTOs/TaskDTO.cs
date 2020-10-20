using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatorId { get; set; }
        public string ExecutorId { get; set; }
        public int StatusId { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<FileDTO> Files { get; set; }
    }
}
