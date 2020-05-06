using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FileApi.Helper;
using FileApi.Interface;
using FileApi.Models;
using Microsoft.AspNetCore.Http;

namespace FileApi.Classes
{
    public class FileWriter : IFileWriter
    {
        public async Task<Result> UploadFile(FileModel file)
        {
            return await WriteFile(file);
        }

        /// <summary>
        /// Чтение и Запись данных
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<Result> WriteFile(FileModel files)
        {
            var result = new Result() { Status = HttpStatusCode.InternalServerError, Message = "Internal Error" };
            string fileName;
            try
            {
                //Upload Files
                if (files.Files != null)
                {
                    foreach (IFormFile file in files.Files)
                    {
                        fileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                            Guid.NewGuid().ToString() + "_" + file.FileName);
                        using (var stream = new FileStream(fileName, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }

                //Upload Base64 images
                if (!String.IsNullOrEmpty(files.FilePath))
                {
                    var bytes = Convert.FromBase64String(files.FilePath);

                    fileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                        Guid.NewGuid().ToString() + "." + WriterHelper.GetFormat(bytes));
                    ProcessBytes(bytes, fileName);
                }

                //Upload from URL
                if (!String.IsNullOrEmpty(files.Url))
                {
                    using (var client = new HttpClient())
                    {
                        using (var response = await client.GetAsync(files.Url))
                        {
                            var bytesFromUrl = await response.Content.ReadAsByteArrayAsync();
                            fileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                                Guid.NewGuid().ToString() + "_" + WriterHelper.GetPathNameFromUrl(files.Url));
                            ProcessBytes(bytesFromUrl, fileName);
                        }
                    }
                }

                if(String.IsNullOrEmpty(files.Url)
                    && String.IsNullOrEmpty(files.FilePath)
                    && files.Files == null)
                {
                    result.Status = HttpStatusCode.NotFound;
                    result.Message = "Files not Found";
                }
                else
                {
                    result.Status = HttpStatusCode.OK;
                    result.Message = "Successfully send";
                }

            }
            catch (Exception e)
            {
                result.Status = HttpStatusCode.InternalServerError;
                result.Message += e.Message;
            }

            return result;
        }

        private void ProcessBytes(byte[] bytes, string fileName)
        {
            

            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
        }
    }
}
