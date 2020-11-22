using BusinessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFileService
    {
        byte[] ExportTaskToCSV(int taskId);
        public FileDownloadDTO GetFile(int id);
        void UploadFile(IFormFile file, int TaskId);
        IEnumerable<DataLayer.Entities.File> GetFilesByTaskId(int taskId);
        void Delete(int id);
    }
}
