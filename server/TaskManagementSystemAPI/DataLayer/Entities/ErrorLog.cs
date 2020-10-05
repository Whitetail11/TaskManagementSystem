using DataLayer.Identity;
using System;

namespace DataLayer.Entities
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime Date{ get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
