using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class CreateUserDTO: RegisterDTO
    {
        public string Role { get; set; }
    }
}
