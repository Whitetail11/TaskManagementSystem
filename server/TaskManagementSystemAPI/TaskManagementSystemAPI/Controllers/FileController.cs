using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Ionic.Zip;
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
            var executorEmail = _tasksService.FindExecutorEmailById(task.ExecutorId);
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
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            fileService.Delete(id);
            return Ok();
        }
        [HttpPost]
        public IActionResult UploadFile(IFormFile Data, int TaskId)
        {
            fileService.UploadFile(Data, TaskId);
            return Ok();
        }
        [Route("getFilesByTaskId")]
        [HttpGet]
        public IActionResult getFilesByTaskId(int taskId)
        {
                var res = fileService.GetFilesByTaskId(taskId).ToArray();
                if(res.Length > 0)
                {
                    var outputStream = new MemoryStream();
                    using (var zip = new ZipFile())
                    {
                        foreach (var value in res)
                        {
                            //string s = System.Text.Encoding.UTF8.GetString(value.Data, 0, value.Data.Length);
                            //zip.AddEntry(value.Name, s);
                            zip.AddEntry(value.Name, value.Data);
                        }
                        zip.Save(outputStream);
                    }
                    outputStream.Position = 0;
                    return File(outputStream, "application/zip", "filename.zip");
                }
                ModelState.AddModelError("HasNoFile", "Has no files");
                return BadRequest(ModelState);
        }
    }
}
