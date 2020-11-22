using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interfaces
{
    public interface IFileRepository: IRepository<File>
    {
        File GetFile(int id);
        IEnumerable<string> GetFileNames(int taskId);
        void Create(File file);
        IEnumerable<File> GetFilesByTaskId(int taskId);
        void Delete(int id);
    }
}
