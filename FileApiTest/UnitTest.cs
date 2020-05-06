using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FileApi;
using FileApi.Classes;
using FileApi.Controllers;
using FileApi.Handler;
using FileApi.Interface;
using FileApi.Models;
using Xunit;

namespace FileApiTest
{
    public class UnitTest
    {
        static readonly string textFile = "base64.txt";

        FileController _controller;
        IFileHandler _fileHandler;
        IFileWriter _fileWriter;
       
        public UnitTest()
        {
            _fileWriter = new FileWriter();
            _fileHandler = new FileHandler(_fileWriter);
            _controller = new FileController(_fileHandler);
        }

        [Fact]
        public void Test1()
        {
            FileModel file = new FileModel() { Url = "https://en.wikipedia.org/wiki/Image#/media/File:Image_created_with_a_mobile_phone.png" };
            Task<Result> result = _controller.File(file);
            Assert.Equal(result.Result.Status, HttpStatusCode.OK);
        }

        [Fact]
        public void Test2()
        {
            string pathToText = Path.Combine("/Users/apple/Projects/FileAPI/FileApiTest", "static", textFile);
            string base64 = File.ReadAllText(pathToText);
            FileModel file = new FileModel() { FilePath = base64 };
            Task<Result> result = _controller.File(file);
            Assert.Equal(result.Result.Status, HttpStatusCode.OK);
        }
    }
}
