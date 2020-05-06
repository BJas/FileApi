using System;
using System.Threading.Tasks;
using FileApi.Interface;
using FileApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileApi.Handler
{
    public interface IFileHandler
    {
        Task<Result> UploadFile(FileModel file);
    }
    public class FileHandler : IFileHandler
    {
        private readonly IFileWriter _fileWriter;
        public FileHandler(IFileWriter fileWriter)
        {
            _fileWriter = fileWriter;
        }

        public async Task<Result> UploadFile(FileModel file)
        {
            var result = await _fileWriter.UploadFile(file);
            return new Result(result.Status, result.Message);
        }
    }
}
