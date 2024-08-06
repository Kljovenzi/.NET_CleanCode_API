using Filip_Furniture.Domain.Common.Generics;
using Filip_Furniture.Service.External_Services.Requests;
using Filip_Furniture.Service.External_Services.Responses;
using Filip_Furniture.Service.External_Services.Responses.Flutterwave;
using Filip_Furniture.Service.External_Services.Responses.Flutterwave.VerifyPaymentResponse;
using Filip_Furniture.Service.External_Services.Responses.Flutterwave.VerifyResponse;
using Filip_Furniture.Service.External_Services.Responses.Verification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Interfaces
{
    public interface IPaymentService
    {
        Task<NewResult<PaystackPaymentInitiationResponse>> InitiatePayment(double amount, string email);
        Task<NewResult<PaystackPaymentVerificationResponse>> VerifyPayment(string reference);
        Task<NewResult<FlutterwaveInitiateCardPaymentResponse>> InitiateFlutterwaveCardPayment(FlutterwaveInitiateCardPaymentRequest request);
        Task<NewResult<FlutterwaveValidateChargeResponse>> FlutterwaveValidateCharge(FlutterwaveValidateChargeRequest request);
        Task<NewResult<FlutterwaveVerifyCardPaymentResponse>> FlutterwaveVerifyCardPayment(FlutterwaveVerifyCardPaymentRequest request);
    }
}
