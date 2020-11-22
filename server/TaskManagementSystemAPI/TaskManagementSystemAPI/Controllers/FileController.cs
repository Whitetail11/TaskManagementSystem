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
using TaskManagementSystemAPI.Extensions;

namespace TaskManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ITaskService _tasksService;
        private readonly IFileService _fileService;

        public FileController(ITaskService tasksService, IFileService fileService)
        {
            _tasksService = tasksService;
            _fileService = fileService;
        }

        [Route("ExportTaskToCSV/{taskId}")]
        [HttpGet]
        public IActionResult ExportTaskToCSV(int taskId)
        {
            if (!_tasksService.HasUserAccess(taskId, HttpContext.GetUserId()))
            {
                return NotFound();
            }
            return File(_fileService.ExportTaskToCSV(taskId), "application/csv");
        }

        [Route("downloadFile")]
        [HttpGet]
        public FileResult DownloadFile(int id)
        {
            var file = _fileService.GetFile(id);
            return File(file.Data, file.ContentType, file.Name);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _fileService.Delete(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile Data, int TaskId)
        {
            _fileService.UploadFile(Data, TaskId);
            return Ok();
        }

        [Route("getFilesByTaskId")]
        [HttpGet]
        public IActionResult getFilesByTaskId(int taskId)
        {
                var res = _fileService.GetFilesByTaskId(taskId).ToArray();
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
