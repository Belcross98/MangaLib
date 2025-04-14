
using System.Net;

namespace vaporAPI.Helpers
{
    public class ApiResponse<T>
    {
        public string Message { get; set; } = String.Empty;
        public bool Success { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }


        public ApiResponse(T? data, bool success, string message, HttpStatusCode? code)
        {
            Data = data;
            Success = success;
            Message = message;
            StatusCode = (int)code;
        }

    }



}