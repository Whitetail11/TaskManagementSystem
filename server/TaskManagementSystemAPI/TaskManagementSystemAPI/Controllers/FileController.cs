using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ITaskService _tasksService;
        private readonly IFileService fileService;
        private readonly IWebHostEnvironment _appEnviroment;

        public FileController(ITaskService _tasksService, IFileService fileService, IWebHostEnvironment env)
        {
            this._tasksService = _tasksService;
            this.fileService = fileService;
            this._appEnviroment = env;
        }
        [Route("downloadCSV")]
        [HttpGet]
        public FileResult DownloadCSV(int id)
        {
            string path = Path.Combine(_appEnviroment.ContentRootPath, String.Format("FilesWithCSV\\newcsv{0}.csv", id));
            var task = _tasksService.GetTaskById(id);
            var executorEmail = "";
            var FileNames = fileService.GetFileNames(id);
            var result = fileService.CreateCsv(id, executorEmail, FileNames, path);
            return File(result, "application/csv", $"Task{id}.csv");
        }
        [Route("downloadFile")]
        [HttpGet]
        public FileResult DownloadFile(int id)
        {
            var file = fileService.GetFile(id);
            return File(file.Data, file.ContentType, file.Name);
        }
    }
}
