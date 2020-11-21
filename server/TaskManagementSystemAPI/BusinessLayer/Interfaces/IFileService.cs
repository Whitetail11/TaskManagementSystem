using BusinessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFileService
    {
        public byte[] CreateCsv(int id, string executorEmail, ICollection<string> FileNames, string path);
        public ICollection<string> GetFileNames(int id);
        public FileDownloadDTO GetFile(int id);
        void UploadFile(IFormFile file, int TaskId);
        IEnumerable<DataLayer.Entities.File> GetFilesByTaskId(int taskId);
        void Delete(int id);
    }
}
