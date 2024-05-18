using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Skinet.API.Errors;

namespace Skinet.API.Controllers
{
    public class BuggyController(StoreContext _context):BaseApiController
    {

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(43);
            if(thing==null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();

        }
        [HttpGet("getServerError")]
        public ActionResult GetServerError()
        {
            var thing = _context.Products.Find(43);
            var thingToReturn = thing.ToString();
            return Ok();

        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
           return BadRequest(new ApiResponse(400));
        }
        [HttpGet("getnotfound")]
        public ActionResult GetNotFound()
        {

            return Ok();
        }
    }
}
