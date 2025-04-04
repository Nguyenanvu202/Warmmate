using API.Controllers.ProductRelatedController;
using API.Extensions;
using API.SignalR;
using AutoMapper;
using Core.Entities;
using Core.Entities.Momo;
using Core.Entities.OrderAggregate;
using Core.IRepository;
using Core.Specifications;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    public class PaymentController : BaseAPIController
    {
        private readonly IMomoService momoService;
        private readonly IUnitOfWork unitOfWork;

        public PaymentController(IMapper mapper, IMomoService momoService, IUnitOfWork unitOfWork)
        : base(mapper)  // Pass mapper to base controller
        {
            this.momoService = momoService;
            this.unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<string>> CreatePaymentMomo(string cartId)
        {
            
            var response = await momoService.CreatePaymentAsync(cartId);
            
            return Ok(response);
        }
        [Authorize]

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            return Ok(await unitOfWork.Repository<DeliveryMethod>().GetAllAsync());
        }



    }
}
