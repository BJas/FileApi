using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileApi.Handler;
using FileApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileApi.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly IFileHandler _fileHandler;

        public FileController(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

        //POST api/file
        [HttpPost]
        public async Task<Result> File([FromForm]FileModel file)
        {
            return await _fileHandler.UploadFile(file);
        }
    }
}
