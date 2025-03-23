using System.Security.Claims;
using API.Controllers.ProductRelatedController;
using API.DTOs;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class BugHandleController(IMapper mapper) : BaseAPIController(mapper)
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

        [Authorize]
        [HttpGet("secret")]
        public IActionResult GetSecret(){
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok("Hello " + name + " with the id: "+ id);
        }

        
    }
}
