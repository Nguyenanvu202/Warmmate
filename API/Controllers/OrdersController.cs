using API.Controllers.ProductRelatedController;
using API.DTOs;
using API.Extensions;
using API.SignalR;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.IRepository;
using Core.Specifications;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    public class OrdersController : BaseAPIController
    {
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NotificationHub> hubContext;

        public OrdersController(
            IMapper mapper,
            ICartService cartService,
            IUnitOfWork unitOfWork,
            IHubContext<NotificationHub> hubContext) : base(mapper)
        {
            _mapper = mapper;
            _cartService = cartService;
            _unitOfWork = unitOfWork;
            this.hubContext = hubContext;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDTO orderDTO)
        {
            var email = User.GetEmail();
            var cart = await _cartService.GetShoppingCartAsync(orderDTO.CartId);
            if (cart == null) return BadRequest();

            var items = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var productItem = await _unitOfWork.Repository<ProductItem>().GetByIdAsync(item.productId);
                if (productItem == null)
                {
                    return BadRequest("Problem with order");
                }

                var itemOrdered = new ProductItemOrdered
                {
                    ProductId = item.productId,
                    ProductName = item.ProductName,
                    PictureUrl = item.PictureUrl
                };
                var orderItem = new OrderItem
                {
                    ItemOrdered = itemOrdered,
                    Price = productItem.Price,
                    Quantity = item.Quantity
                };
                items.Add(orderItem);
            }

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(orderDTO.DeliveryMethodId);
            if (deliveryMethod == null) return BadRequest("No delivery method selected");
            var order = new Order
            {
                OrderItems = items,
                DeliveryMethod = deliveryMethod,
                ShippingAddress = orderDTO.ShippingAddress,
                Subtotal = items.Sum(x => x.Price * x.Quantity),
                BuyerEmail = email
            };

            _unitOfWork.Repository<Order>().Add(order);

            var connectionId = NotificationHub.GetConnectionIdByEmail(order.BuyerEmail);

            if (!string.IsNullOrEmpty(connectionId))
            {
                var orderDto = _mapper.Map<OrderDTO>(order);
                await hubContext.Clients.Client(connectionId)
                    .SendAsync("OrderCompleteNotification", orderDto);
            }
            if (await _unitOfWork.Complete())
            {
                return order;
            }

            return BadRequest("Problem creating order");
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDTO>>> GetOrdersForUser()
        {
            var spec = new OrderSpecification(User.GetEmail());
            var orders = await _unitOfWork.Repository<Order>().GetAllBySpec(spec);
            var ordersToReturn = _mapper.Map<List<OrderDTO>>(orders);
            return Ok(ordersToReturn);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            var spec = new OrderSpecification(User.GetEmail(), id);
            var order = await _unitOfWork.Repository<Order>().GetBySpec(spec);
            if (order == null) return NotFound();
            var orderDTO = _mapper.Map<OrderDTO>(order);
            return orderDTO;
        }
    }
}