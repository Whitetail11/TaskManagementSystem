using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class FileDTO
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public DateTime AttachedDate { get; set; }
    }
}
