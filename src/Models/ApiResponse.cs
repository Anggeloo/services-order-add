namespace services_order_add.Models
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ApiResponse(string status, T data, string message)
        {
            Status = status;
            Data = data;
            Message = message;
        }
    }
}
