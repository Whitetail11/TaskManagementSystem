using BusinessLayer.DTOs;
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
    }
}
