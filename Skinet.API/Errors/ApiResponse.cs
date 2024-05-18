
namespace Skinet.API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statuesCode, string message=null)
        {
            StatuesCode = statuesCode;
            Message = message ?? GetDefaultMessageForStatuesCode(statuesCode);
        }

        public int StatuesCode { get; set; }    
        public string Message { get; set; }
        private string GetDefaultMessageForStatuesCode(int statuesCode)
        {
            return statuesCode switch
            {
                400 => "A bad request, you have made ",
                401 => "Authrized , you are not ",
                404 => "Resources found ,it was not ",
                500 => @"Errors are the path to the dark side Errors lead to anger , Anger leads to 
                hate . Hate leads to career change",
                _=>null
            };
        }

    }
}
