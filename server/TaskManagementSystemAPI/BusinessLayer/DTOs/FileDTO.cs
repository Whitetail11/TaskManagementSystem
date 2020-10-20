using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class FileDTO
    {
        public int Id { get; set; }
        public TaskDTO Task { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public DateTime AttachedDate { get; set; }

    }
}
