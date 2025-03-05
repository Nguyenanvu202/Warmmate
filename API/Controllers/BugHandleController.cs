using API.Controllers.ProductRelatedController;
using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class BugHandleController : BaseAPIController
    {
        [HttpGet("unauthorize")]
        public IActionResult GetUnauthorize()
        {
            return Unauthorized();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("Not a good request");
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("internalerror")]
        public IActionResult GetInternalError()
        {
            throw new Exception("This is a test fully exception");
        }
        
        [HttpPost("validationerror")]
        public IActionResult GetValidationError(ItemDTO item)
        {
            return Ok();
        }
    }
}
