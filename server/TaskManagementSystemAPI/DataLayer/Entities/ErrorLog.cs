using System;

namespace DataLayer.Entities
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime Date{ get; set; }

        public virtual User User { get; set; }
    }
}
