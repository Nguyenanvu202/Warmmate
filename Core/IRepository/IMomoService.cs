using System;
using Core.Entities;
using Core.Entities.Momo;
using Microsoft.AspNetCore.Http;

namespace Core.IRepository;

public interface IMomoService
{
    Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(string cartId);
    MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
}
