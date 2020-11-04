using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class ShowTaskDTO
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public string CreatorName { get; set; }
        public string ExecutorName { get; set; }
        public IEnumerable<ShowCommentDTO> Comments { get; set; }
        public IEnumerable<FileDTO> Files { get; set; }
    }
}
