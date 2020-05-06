using System;
using System.Collections.Generic;
using System.Net;

namespace FileApi.Models
{
    /// <summary>
    /// Результат выполнения запроса с кодом и описанием
    /// </summary>
    public class Result
    {
        public Result()
        {
            Status = HttpStatusCode.InternalServerError;
        }

        public Result(HttpStatusCode status, string message = "")
        {
            Status = status;
            Message = message;
        }

        public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;

        public string Message { get; set; }
    }
}
