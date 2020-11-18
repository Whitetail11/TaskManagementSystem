using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public class StatusRepository: Repository<Status>, IStatusRepository
    {
        public StatusRepository(ApplicationContext dbContext): base(dbContext) 
        {}

        public string GetName(int id)
        {
            return _dbContext.Statuses.AsNoTracking()
                .Where(status => status.Id == id)
                .Select(status => status.Name)
                .FirstOrDefault();
        }
    }
}
