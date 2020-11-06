using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class ShowCommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public int? ReplyCommentId { get; set; }
        public string Date { get; set; }

        public string ReplyUserName { get; set; }
        public string UserName { get; set; }
        public List<ShowCommentDTO> Replies { get; set; }
    }
}
