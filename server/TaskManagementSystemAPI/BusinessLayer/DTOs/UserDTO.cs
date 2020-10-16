using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual ICollection<TaskDTO> Tasks { get; set; }
        public virtual ICollection<TaskDTO> ExecutorTasks { get; set; }
        public virtual ICollection<CommentDTO> Comments { get; set; }
        public virtual ICollection<ErrorLogDTO> Errors { get; set; }

    }
}
