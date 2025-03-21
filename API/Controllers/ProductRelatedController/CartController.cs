using AutoMapper;
using Core.Entities;
using Core.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedController
{

    public class CartController(IMapper mapper, ICartService cartService) : BaseAPIController(  mapper)
    {
        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById(string id)
        {
            var cart = await cartService.GetShoppingCartAsync(id);
            return Ok(cart ?? new ShoppingCart{ Id = id });
        }


        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
        {
            var updatedCart = await cartService.SetShoppingCartAsync(cart);

            if (updatedCart == null)
            {
                return BadRequest("Proplem with cart");

            }
            return updatedCart;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCart(string id)
        {
            var result = await cartService.DeleteCartAsync(id);

            if (!result) return BadRequest("Problem with cart");

            return Ok();
        }
    }
}