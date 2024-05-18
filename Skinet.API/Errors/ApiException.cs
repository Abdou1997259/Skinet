namespace Skinet.API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statuesCode, string message = null, string details = null) : base(statuesCode, message)
        {
            Details = details;
        }
        public string Details { get; set; }
    }
}
