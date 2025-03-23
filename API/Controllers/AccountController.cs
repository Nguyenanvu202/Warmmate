using System.Security.Claims;
using API.Controllers.ProductRelatedController;
using API.DTOs;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IMapper mapper, SignInManager<AppUser> signInManager) : BaseAPIController(mapper)
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO register)
        {
            var user = mapper.Map<AppUser>(register);
            var result = await signInManager.UserManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }

            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated == false)
            {
                return NoContent();
            }
            var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);
            var address = mapper.Map<AddressDTO>(user.Address);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                address
            });


        }

        [HttpGet]
        public ActionResult GetAuthState(){
            return Ok(new{ IsAuthenticated = User.Identity?.IsAuthenticated ?? false});
        }

        [Authorize]
        [HttpPost("address")]
        public async Task<ActionResult> CreateOrUpdateAdress(AddressDTO addressDTO)
        {
            var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);

             user.Address = mapper.Map<Address>(addressDTO);
            
            var result = await signInManager.UserManager.UpdateAsync(user);
            if(!result.Succeeded){
                return BadRequest("Problem with updating user address");
            }

            return Ok(addressDTO);
        }

    }
}
