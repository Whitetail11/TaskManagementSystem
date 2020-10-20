using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public TaskDTO Task { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
