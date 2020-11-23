using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class CreateCommentDTO
    {
        public string Text { get; set; }
        public int TaskId { get; set; }
        public int? ReplyCommentId { get; set; }
    }
}
