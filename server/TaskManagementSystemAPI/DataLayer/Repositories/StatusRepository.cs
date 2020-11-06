using DataLayer.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public class StatusRepository: Repository<Status>, IStatusRepository
    {
        public StatusRepository(ApplicationContext dbContext): base(dbContext) 
        {}
    }
}
