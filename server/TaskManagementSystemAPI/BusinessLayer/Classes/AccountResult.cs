using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Classes
{
    public class AccountResult
    {
        public bool Succeeded { get; }
        public string Token { get; }
        public List<string> Errors { get; }

        public AccountResult(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public AccountResult(bool succeeded, string token)
        {
            Succeeded = succeeded;
            Token = token;
        }

        public AccountResult(List<string> errors)
        {
            Errors = errors;
        }
    }
}
