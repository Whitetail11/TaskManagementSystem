using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Classes
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                dbContext.AddRange(new List<IdentityRole>() 
                { 
                    new IdentityRole(){
                        Id = "1", 
                        Name = ApplicationConstants.Roles.ADMINISTRATOR, 
                        NormalizedName = ApplicationConstants.Roles.ADMINISTRATOR
                    },
                    new IdentityRole(){
                        Id = "2",
                        Name = ApplicationConstants.Roles.CUSTOMER,
                        NormalizedName = ApplicationConstants.Roles.CUSTOMER
                    }, 
                    new IdentityRole(){
                        Id = "3",
                        Name = ApplicationConstants.Roles.EXECUTOR,
                        NormalizedName = ApplicationConstants.Roles.EXECUTOR
                    },
                });
                dbContext.SaveChanges();
            }
        } 
    }
}
