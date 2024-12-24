using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MS.CoachSystem.Core.DTOs
{
    public class GenericResponse<T> where T : class
    {
        public T Data { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public ErrorDto Error { get; set; }

        [JsonIgnore]
        public bool IsSuccessfull { get; set; }

        public static GenericResponse<T> Success(T data, HttpStatusCode statusCode)
        {
            return new GenericResponse<T>()
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessfull = true
            };
        }

        public static GenericResponse<T> Success(HttpStatusCode statusCode)
        {
            return new GenericResponse<T>()
            {
                StatusCode = statusCode,
                IsSuccessfull = true
            };
        }

        public static GenericResponse<T> Fail(ErrorDto errorDto, HttpStatusCode statusCode)
        {
            return new GenericResponse<T>()
            {
                Error = errorDto,
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }

        public static GenericResponse<T> Fail(HttpStatusCode statusCode)
        {
            return new GenericResponse<T>()
            {
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }

        public static GenericResponse<T> Fail(string errorMessage, HttpStatusCode statusCode, bool isShow)
        {
            var errorDto = new ErrorDto(errorMessage, isShow);
            return new GenericResponse<T>()
            {
                Error = errorDto,
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }
    }
}
