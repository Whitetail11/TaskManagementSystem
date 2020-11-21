using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class DeleteUserDTO
    {
        [Required]
        public string Password { get; set; }
    }
}
