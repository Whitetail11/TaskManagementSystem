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
        public void Create(File file)
        {
            _dbContext.Files.Add(file);
            _dbContext.SaveChanges();
        }
        public IEnumerable<File> GetFilesByTaskId(int taskId)
        {
            return _dbContext.Files
                .Where(f => f.TaskId == taskId).ToList();
        }
        public void Delete(int id)
        {
            var res = _dbContext.Files.FirstOrDefault(f => f.Id == id);
            _dbContext.Files.Remove(res);
            _dbContext.SaveChanges();
        }
    }
}
