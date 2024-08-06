using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Service.External_Services.Requests;
using Filip_Furniture.Service.External_Services.Responses.Flutterwave;
using Filip_Furniture.Service.External_Services.Responses.Flutterwave.VerifyPaymentResponse;
using Filip_Furniture.Service.External_Services.Responses.Flutterwave.VerifyResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.External_Services.Interfaces
{
    public interface IFlutterwaveService
    {
        Task<FlutterwaveInitiateCardPaymentResponse> InitiatePaymentAsync(FlutterwaveInitiateCardPaymentRequest request, MyBankLog log);
        Task<FlutterwaveValidateChargeResponse> ValidateChargeAsync(FlutterwaveValidateChargeRequest request, MyBankLog log);
        Task<FlutterwaveVerifyCardPaymentResponse> VerifyPaymentAsync(FlutterwaveVerifyCardPaymentRequest request, MyBankLog log);
    }
}
