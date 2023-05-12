namespace TaskApp.WebApi.Results
{
    public sealed class ApiResponse<T>
    {
        public StatusType Status { get; set; }
        public string ResultMessage { get; set; }
        public int ErrorCode { get; set; } 
        public T Data { get; set; }
    }

    public enum StatusType
    {
        Success,
        Failed
    }    
}
