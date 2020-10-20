using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationContext _dbContext;
        public Repository(ApplicationContext context)
        {
            this._dbContext = context;
        }
        public List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }
    }
}
