namespace Dhoojol.Api.Helpers
{
    public class ApiResult <T>
    {
        public T? Data { get;}
        public bool Success { get;} = true;
        public string? Message { get; }

        public ApiResult(T? data)
            {
            Data = data;
            }
        public ApiResult(string? message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }
            Message = message;
        }
    }

    
    public class ApiResult
    {
        //sucre
        public static ApiResult<T> Success<T>(T data)
        {
            return new ApiResult<T>(data);
        }
        public static ApiResult<T> Failed<T>(string message)
        {
            return new ApiResult<T>(message);
        }
    }
}
