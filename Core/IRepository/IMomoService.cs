using System;
using Core.Entities;
using Core.Entities.Momo;
using Core.Entities.OrderAggregate;
using Microsoft.AspNetCore.Http;

namespace Core.IRepository;

public interface IMomoService
{
    Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(string cardId);
     MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
     string ComputeHmacSha256(string message, string secretKey);
}
