using API.Controllers.ProductRelatedController;
using AutoMapper;
using Core.Entities;
using Core.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaymentController(IMapper mapper, IMomoService momoService, IGenericRepo<DeliveryMethod> dmRepo) : BaseAPIController(mapper)
    {

        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<string>> CreatePaymentMomo(string cartId){
           var response = await momoService.CreatePaymentAsync(cartId);
           return Ok(response);
        }

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod(){
            return Ok(await dmRepo.GetAllAsync());
        }

    }
}
