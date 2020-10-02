using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Migrations.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public Task Task { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int ReplyCommentId { get; set; }
        [ForeignKey("ReplyCommentId")]
        public Comment ReplyComment { get; set; }
    }
}
