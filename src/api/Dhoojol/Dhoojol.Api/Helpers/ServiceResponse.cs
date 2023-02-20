namespace Dhoojol.Api.Helpers
{
    public class ServiceResponse <T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;


    }
    public class ServiceResponse
    {
        //sucre
        public static ServiceResponse<T> Success<T>(T data)
        {
            return new ServiceResponse<T> { Data = data };
        }
        public static ServiceResponse<string> Failed(string message)
        {
            return new ServiceResponse<string> { Success = false, Message = message };
        }
    }
}
