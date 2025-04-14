
namespace vaporAPI.Helpers
{
    public class ApiResponse<T>
    {
        public string Message { get; set; } = String.Empty;
        public bool Success { get; set; }
        public T? Data { get; set; }


        public ApiResponse(T? data, bool success, string message)
        {
            Data = data;
            Success = success;
            Message = message;

        }
    }



}