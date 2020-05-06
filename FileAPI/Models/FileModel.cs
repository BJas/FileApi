using System;
using Microsoft.AspNetCore.Http;

namespace FileApi.Models
{
    public class FileModel
    {
        public IFormFile[] Files { get; set; }
        public string FilePath { get; set; }
        public string Url { get; set; }
    }
}
