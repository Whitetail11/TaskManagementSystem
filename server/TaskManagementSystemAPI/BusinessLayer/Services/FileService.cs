using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using CsvHelper;
using DataLayer.Entities;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace BusinessLayer.Services
{
    public class FileService: IFileService
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;
        private readonly IFileRepository _fileRepository;

        public FileService(ITaskService taskService, IMapper mapper, IFileRepository fileRepository)
        {
            _taskService = taskService;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }
    
        public byte[] ExportTaskToCSV(int taskId)
        {
            var taskCSV = _taskService.GetForCSVExporting(taskId);
            taskCSV.Files = string.Join(";", _fileRepository.GetFileNames(taskId));

            byte[] data;
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteHeader<TaskCSVDTO>();
                    csvWriter.NextRecord();
                    csvWriter.WriteRecord(taskCSV);
                }
                data = memoryStream.ToArray();
            }
            return data;
        }

        public FileDownloadDTO GetFile(int id)
        {
            return _mapper.Map<FileDownloadDTO>(_fileRepository.GetFile(id));
        }

        public IEnumerable<DataLayer.Entities.File> GetFilesByTaskId(int taskId)
        {
            return _fileRepository.GetFilesByTaskId(taskId);
        }

        public void Delete(int id)
        {
            _fileRepository.Delete(id);
        }

        public void UploadFile(IFormFile file, int TaskId)
        {
            DataLayer.Entities.File result = new DataLayer.Entities.File {
                AttachedDate = DateTime.Now,
                Name = file.FileName,
                TaskId = TaskId,
                Id = 0,
                ContentType = file.ContentType
            };
            if(file != null)
            {
                byte[] data = null;
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    data = binaryReader.ReadBytes((int)file.Length);
                }
                result.Data = data;
            }
            _fileRepository.Create(result);
        }
    }
}
