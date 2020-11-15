using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Identity
{
    public class ApplicationUserManager: UserManager<ApplicationUser>
    {
        private readonly ApplicationContext _dbContext;

        public ApplicationUserManager(ApplicationContext dbContext, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, 
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _dbContext = dbContext;
        }

        public async Task SetEmailAsNotConfirmed(ApplicationUser user)
        {
            user.EmailConfirmed = false;
            _dbContext.Users.Attach(user);
            _dbContext.Entry(user).Property(user => user.EmailConfirmed).IsModified = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
