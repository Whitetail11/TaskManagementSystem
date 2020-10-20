using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class StatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TaskDTO> Tasks { get; set; }
    }
}
