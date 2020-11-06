using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using CsvHelper;
using DataLayer.Interfaces;
using DataLayer.Repositories;
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
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IFileRepository _fileRepository;

        public FileService(ITaskRepository _taskRepository, IMapper _mapper, IFileRepository fileRepository)
        {
            this._taskRepository = _taskRepository;
            this._mapper = _mapper;
            this._fileRepository = fileRepository;
        }
        public ICollection<string> GetFileNames(int id)
        {
            var task = _taskRepository.GetTaskById(id);
            try
            {
                ICollection<string> FileNames = task.Files.Select(o => o.Name).ToList();
                return FileNames;
            }
            catch(Exception ex)
            {
                return null;
            }

        }
        public byte[] CreateCsv(int id, string executorEmail, ICollection<string> FileNames, string path)
        {
            var task = _taskRepository.GetTaskById(id);
            System.IO.File.Create(path).Close();
            TaskCsvDTO taskInfo = new TaskCsvDTO();
            taskInfo.Deadline = task.Deadline;
            taskInfo.Description = task.Description;
            taskInfo.Title = task.Title;
            taskInfo.ExecutorEmail = executorEmail;
            taskInfo.FileNames = FileNames;
            using (StreamWriter streamReader = new StreamWriter(path))
            {
                //streamReader.WriteLine("sep=,");//добавить для корректного открытия в екселе
                // также для корректого открытия в екселе нужно поменять в нем кодировку
                // файлы корректно открываются в других программах, например 
                using (CsvWriter csvReader = new CsvWriter(streamReader, new CultureInfo("")))
                {
                    // указываем разделитель, который будет использоваться в файле
                    csvReader.Configuration.Delimiter = ",";
                    // Записываем заголовки столбцов
                    csvReader.WriteHeader<TaskCsvDTO>();
                    csvReader.NextRecord();
                    // записываем данные в csv файл
                    csvReader.WriteRecord<TaskCsvDTO>(taskInfo);
                }

            }
            byte[] mas = System.IO.File.ReadAllBytes(path);
            return mas;
        }


        public FileDownloadDTO GetFile(int id)
        {
            return _mapper.Map<FileDownloadDTO>(_fileRepository.GetFile(id));
        }
    }
}
