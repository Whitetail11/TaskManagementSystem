using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLayer.DTOs
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Current password is required.")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Password confirmation is required.")]
        [Compare("NewPassword", ErrorMessage = "Password do not match.")]
        public string PasswordConfirm { get; set; }
    }
}
