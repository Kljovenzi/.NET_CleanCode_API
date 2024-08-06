using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Service.External_Services.Requests;
using Filip_Furniture.Service.External_Services.Responses;
using Filip_Furniture.Service.External_Services.Responses.Verification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Interfaces
{
    public interface IPaystackService
    {
        Task<PaystackPaymentInitiationResponse> InitiatePaymentAsync(PaystackPaymentInitiationRequest request, MyBankLog log);
        Task<PaystackPaymentVerificationResponse> VerifyPaymentAsync(PaystackPaymentVerificationRequest request, MyBankLog log);
    }
}
