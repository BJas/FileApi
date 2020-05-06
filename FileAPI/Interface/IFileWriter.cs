using System;
using System.Threading.Tasks;
using FileApi.Models;
using Microsoft.AspNetCore.Http;

namespace FileApi.Interface
{
    public interface IFileWriter
    {
        Task<Result> UploadFile(FileModel file);
    }
}
