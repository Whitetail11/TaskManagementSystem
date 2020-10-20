using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class ErrorLogDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime Date { get; set; }
    }
}
