using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public class FileRepository: Repository<File>, IFileRepository
    {
        public FileRepository(ApplicationContext context)
            : base(context)
        { }
        public File GetFile(int id)
        {
            return _dbContext.Files.AsNoTracking().FirstOrDefault(f => f.Id == id);
        }
    }
}
