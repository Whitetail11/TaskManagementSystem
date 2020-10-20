using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<TaskDTO> Tasks { get; set; }
        public ICollection<TaskDTO> ExecutorTasks { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<ErrorLogDTO> Errors { get; set; }

    }
}
