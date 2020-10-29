using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TaskManagementSystemAPI.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this HttpContext context)
        {
            return context.User.Claims
                .FirstOrDefault(claim => claim.Type == "userid")?.Value;
        }

        public static string GetUserRole(this HttpContext context)
        {
            return context.User.Claims
                .FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
        }
    }
}
