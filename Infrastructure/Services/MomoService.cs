using System;
using System.Security.Cryptography;
using System.Text;
using Core.Entities;
using Core.Entities.Momo;
using Core.IRepository;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
namespace Infrastructure.Services;

public class MomoService : IMomoService
{
    private readonly IOptions<MomoOptionModel> _options;
    private readonly IUnitOfWork unitOfWork;
    private readonly ICartService cartService;
    private readonly IHubContext hubContext;

    public MomoService(IOptions<MomoOptionModel> options, IUnitOfWork unitOfWork, ICartService cartService, IHubContext hubContext)
    {
        _options = options;
        this.unitOfWork = unitOfWork;
        this.cartService = cartService;
        this.hubContext = hubContext;
    }
    public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(string cartId)
    {

        var cart = await cartService.GetShoppingCartAsync(cartId);
        if (cart == null) return null;

        var shippingPrice = 0m;
        if (cart.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync((int)cart.DeliveryMethodId);
            if (deliveryMethod == null) return null;

            shippingPrice = deliveryMethod.Price;
        }
        foreach (var item in cart.Items)
        {
            var productItem = await unitOfWork.Repository<ProductItem>().GetByIdAsync(item.productId);

            if (productItem == null) return null;

            if (item.Price != productItem.Price)
            {
                item.Price = productItem.Price;
            }
        }


        var OrderId = DateTime.UtcNow.Ticks.ToString();
        cart.OrderId = OrderId;
        await cartService.SetShoppingCartAsync(cart);
        var OrderInfo = "Nội dung: Thanh toán đơn hàng" + cart.Id;
        var amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 1000)) + (long)shippingPrice * 1000;
        var rawData =
            $"partnerCode={_options.Value.PartnerCode}" +
            $"&accessKey={_options.Value.AccessKey}" +
            $"&requestId={OrderId}" +
            $"&amount={amount}" +
            $"&orderId={OrderId}" +
            $"&orderInfo={OrderInfo}" +
            $"&returnUrl={_options.Value.ReturnUrl}" +
            $"&notifyUrl={_options.Value.NotifyUrl}" +
            $"&extraData=";

        var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

        var client = new RestClient(_options.Value.MomoApiUrl);
        var request = new RestRequest() { Method = Method.Post };
        request.AddHeader("Content-Type", "application/json; charset=UTF-8");

        // Create an object representing the request data
        var requestData = new
        {
            accessKey = _options.Value.AccessKey,
            partnerCode = _options.Value.PartnerCode,
            requestType = _options.Value.RequestType,
            notifyUrl = _options.Value.NotifyUrl,
            returnUrl = _options.Value.ReturnUrl,
            orderId = cart.OrderId,
            amount = amount.ToString(),
            orderInfo = OrderInfo,
            requestId = cart.OrderId,
            extraData = "",
            signature = signature
        };
        
        request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

        var response = await client.ExecuteAsync(request);
        var momoResponse = JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content);
        return momoResponse;

    }
    public MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection)
    {
        var amount = collection.FirstOrDefault(s => s.Key == "amount").Value;
        var orderInfo = collection.FirstOrDefault(s => s.Key == "orderInfo").Value;
        var orderId = collection.FirstOrDefault(s => s.Key == "orderId").Value;

        return new MomoExecuteResponseModel()
        {
            Amount = amount,
            OrderId = orderId,
            OrderInfo = orderInfo

        };
    }


    public string ComputeHmacSha256(string message, string secretKey)
    {
        var keyBytes = Encoding.UTF8.GetBytes(secretKey);
        var messageBytes = Encoding.UTF8.GetBytes(message);

        byte[] hashBytes;

        using (var hmac = new HMACSHA256(keyBytes))
        {
            hashBytes = hmac.ComputeHash(messageBytes);
        }

        var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        return hashString;
    }
}
