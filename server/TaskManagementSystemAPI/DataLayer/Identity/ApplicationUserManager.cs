using DataLayer.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync(Page page)
        {
            return await _dbContext.Users.AsNoTracking()
                .Skip((page.Number - 1) * page.Size)
                .Take(page.Size)
                .ToListAsync();
        } 

        public async Task<int> GetCountAsync()
        {
            return await _dbContext.Users.AsNoTracking().CountAsync();
        }

        public async Task<bool> ExistAnyAsync(string id)
        {
            return await _dbContext.Users.AsNoTracking().AnyAsync(user => user.Id == id);
        }

        public async Task SetEmailAsNotConfirmedAsync(ApplicationUser user)
        {
            user.EmailConfirmed = false;
            _dbContext.Users.Attach(user);
            _dbContext.Entry(user).Property(user => user.EmailConfirmed).IsModified = true;
            await _dbContext.SaveChangesAsync();
        }

        public string GetFullName(string userId)
        {
            return _dbContext.Users.AsNoTracking()
                .Where(user => user.Id == userId)
                .Select(user => $"{ user.Name } { user.Surname }")
                .FirstOrDefault();
        }
    }
}
