using BusinessLayer.DTOs;
using DataLayer.Identity;
using System.Collections.Generic;

namespace BusinessLayer.Classes
{
    public class AccountResult
    {
        public bool Succeeded { get; }
        public string Token { get; }
        public IEnumerable<string> Errors { get; }
        public ApplicationUser User { get; }

        public AccountResult(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public AccountResult(bool succeeded, ApplicationUser user)
        {
            Succeeded = succeeded;
            User = user;
        }
     
        public AccountResult(bool succeeded, string token)
        {
            Succeeded = succeeded;
            Token = token;
        }

        public AccountResult(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
