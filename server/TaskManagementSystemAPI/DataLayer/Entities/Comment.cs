using DataLayer.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int TaskId { get; set; }
        public string UserId { get; set; }
        public int? ReplyCommentId { get; set; }
        public DateTime Date { get; set; }

        public Task Task { get; set; }
        public ApplicationUser User { get; set; }
    }
}
