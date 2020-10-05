using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystemAPI.Classes
{
    public class AuthOptions
    {
        public const string ISSUER = "ApplicationServer";
        public const string AUDIENCE = "ApplicationClient";
        public const int LIFETIME = 1; // days
        const string KEY = "APRIORIT_INTERNSHIP_2020_TEAM";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
