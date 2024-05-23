using Microsoft.AspNetCore.Mvc;
using Skinet.API.Errors;

namespace Skinet.API.Controllers
{
    [Route("error/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController:ControllerBase
    {
        
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
