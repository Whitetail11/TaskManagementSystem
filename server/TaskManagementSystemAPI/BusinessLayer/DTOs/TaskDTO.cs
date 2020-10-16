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
        public virtual UserDTO Creator { get; set; }
        public virtual UserDTO Executor { get; set; }
        public virtual StatusDTO Status { get; set; }
        public virtual ICollection<CommentDTO> Comments { get; set; }
        public virtual ICollection<FileDTO> Files { get; set; }
    }
}
