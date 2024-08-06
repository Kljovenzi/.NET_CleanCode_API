using Filip_Furniture.Domain.Common.Generics;
using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Service.External_Services.Interfaces;
using Filip_Furniture.Service.External_Services.Requests;
using Filip_Furniture.Service.External_Services.Responses;
using Filip_Furniture.Service.External_Services.Responses.Flutterwave;
using Filip_Furniture.Service.External_Services.Responses.Flutterwave.VerifyPaymentResponse;
using Filip_Furniture.Service.External_Services.Responses.Flutterwave.VerifyResponse;
using Filip_Furniture.Service.External_Services.Responses.Verification;
using Filip_Furniture.Service.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaystackService paystackService;
        //private readonly ISampleRepository sampleRepository;
        //private readonly IMyBankLogRepository myBankLogRepository;
        private readonly ILogger logger;
        private readonly IFlutterwaveService flutterwaveService;
        public PaymentService(IPaystackService paystackService,
                              //ISampleRepository sampleRepository,
                              //IMyBankLogRepository myBankLogRepository,
                              ILogger logger,
                              IFlutterwaveService flutterwaveService)
        {
            this.paystackService = paystackService;
            //this.sampleRepository = sampleRepository;
            //this.myBankLogRepository = myBankLogRepository;
            this.logger = logger;
            this.flutterwaveService = flutterwaveService;
        }

        public async Task<NewResult<FlutterwaveValidateChargeResponse>> FlutterwaveValidateCharge(FlutterwaveValidateChargeRequest request)
        {
            MyBankLog dbLog = new MyBankLog();
            try
            {
                if (string.IsNullOrWhiteSpace(request.Otp))
                {
                    throw new ArgumentNullException(nameof(request.Otp), "OTP cannot be null or empty.");
                }

                if (string.IsNullOrWhiteSpace(request.Flw_ref))
                {
                    throw new ArgumentNullException(nameof(request.Flw_ref), "FLW Reference cannot be null or empty.");
                }

                // Call the Flutterwave service to validate the charge
                var validateChargeResponse = await flutterwaveService.ValidateChargeAsync(request, dbLog);

                // Check the response from the service
                if (validateChargeResponse != null && validateChargeResponse.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
                {
                    return NewResult<FlutterwaveValidateChargeResponse>.Success(validateChargeResponse, "Charge successfully validated.");
                }

                return NewResult<FlutterwaveValidateChargeResponse>.Failed(null, "Charge validation failed.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return NewResult<FlutterwaveValidateChargeResponse>.Error(null, $"Error while validating charge: {ex.Message}");
            }
        }

        public async Task<NewResult<FlutterwaveVerifyCardPaymentResponse>> FlutterwaveVerifyCardPayment(FlutterwaveVerifyCardPaymentRequest request)
        {
            MyBankLog dbLog = new MyBankLog();
            try
            {
                if (string.IsNullOrWhiteSpace(request.TransactionId))
                {
                    throw new ArgumentNullException(nameof(request.TransactionId), "TransactionId cannot be null or empty.");
                }
                var verifyCardPayment = await flutterwaveService.VerifyPaymentAsync(request, dbLog);

                // Check if the response is not null
                if (verifyCardPayment == null)
                {
                    return NewResult<FlutterwaveVerifyCardPaymentResponse>.Failed(null, "Verification failed: no response from payment service.");
                }

                // Check the status of the response
                if (verifyCardPayment.Status != "success")
                {
                    return NewResult<FlutterwaveVerifyCardPaymentResponse>.Failed(null, $"Verification failed: {verifyCardPayment.Message}");
                }

                // Check critical fields in the data
                if (verifyCardPayment.Data == null ||
                    string.IsNullOrWhiteSpace(verifyCardPayment.Data.TxRef) ||
                    string.IsNullOrWhiteSpace(verifyCardPayment.Data.FlwRef))
                {
                    return NewResult<FlutterwaveVerifyCardPaymentResponse>.Failed(null, "Verification failed: missing critical data in response.");
                }

                return NewResult<FlutterwaveVerifyCardPaymentResponse>.Success(verifyCardPayment, "Payment successfully verified.");
            }
            catch (Exception ex)
            {

                logger.Error(ex, ex.Message);
                return NewResult<FlutterwaveVerifyCardPaymentResponse>.Error(null, $"Error while verifying payment: {ex.Message}");

            }
        }

        public async Task<NewResult<FlutterwaveInitiateCardPaymentResponse>> InitiateFlutterwaveCardPayment(FlutterwaveInitiateCardPaymentRequest request)
        {
            MyBankLog dbLog = new MyBankLog();
            try
            {
                if (request.Amount <= 0)
                {
                    throw new ArgumentException("Amount must be greater than zero.");
                }
                if (!IsValidEmail(request.Email))
                {
                    throw new ArgumentException("Invalid email format.", nameof(request.Email));
                }
                var initiatePayment = await flutterwaveService.InitiatePaymentAsync(request, dbLog);
                if (initiatePayment != null && initiatePayment.Status == "success")
                {
                    return NewResult<FlutterwaveInitiateCardPaymentResponse>.Success(initiatePayment, "Payment initiation successful.");
                }
                else
                {
                    return NewResult<FlutterwaveInitiateCardPaymentResponse>.Failed(null, "Payment initiation failed.");
                }

            }
            catch (Exception ex)
            {

                logger.Error(ex, ex.Message);
                return NewResult<FlutterwaveInitiateCardPaymentResponse>.Error(null, $"Error while initiating payment : {ex.Message}");
            }
        }

        public async Task<NewResult<PaystackPaymentInitiationResponse>> InitiatePayment(double amount, string email)
        {
            MyBankLog dbLog = new MyBankLog();
            try
            {
                if (amount <= 0)
                {
                    throw new ArgumentException("Amount must be greater than zero.");
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
                }

                if (!IsValidEmail(email))
                {
                    throw new ArgumentException("Invalid email format.", nameof(email));
                }

                var initiatePaymentRequest = new PaystackPaymentInitiationRequest()
                {
                    Amount = amount,
                    Email = email
                };

                var initiatePayment = await paystackService.InitiatePaymentAsync(initiatePaymentRequest, dbLog);
                // Validate the response
                if (initiatePayment != null && initiatePayment.Status)
                {
                    if (!string.IsNullOrWhiteSpace(initiatePayment.Data.AuthorizationUrl) && !string.IsNullOrWhiteSpace(initiatePayment.Data.Reference))
                    {
                        return NewResult<PaystackPaymentInitiationResponse>.Success(initiatePayment, "Payment successfully initiated.");
                    }

                    return NewResult<PaystackPaymentInitiationResponse>.Failed(null, "Payment initiation response missing critical data.");
                }
                return NewResult<PaystackPaymentInitiationResponse>.Failed(null, "Payment initiation failed.");


            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return NewResult<PaystackPaymentInitiationResponse>.Error(null, $"Error while initiating payment : {ex.Message}");
            }
        }

        public async Task<NewResult<PaystackPaymentVerificationResponse>> VerifyPayment(string reference)
        {
            MyBankLog dbLog = new MyBankLog();
            try
            {
                if (string.IsNullOrWhiteSpace(reference))
                {
                    throw new ArgumentNullException(nameof(reference), "Reference cannot be null or empty.");
                }
                var verifyPaymentRequest = new PaystackPaymentVerificationRequest()
                {
                    Reference = reference
                };
                var verifyPayment = await paystackService.VerifyPaymentAsync(verifyPaymentRequest, dbLog);
                if (verifyPayment != null && verifyPayment.Status)
                {
                    if (verifyPayment.Data != null && !string.IsNullOrWhiteSpace(verifyPayment.Data.Status))
                    {
                        return NewResult<PaystackPaymentVerificationResponse>.Success(verifyPayment, "Payment successfully verified.");
                    }

                    return NewResult<PaystackPaymentVerificationResponse>.Failed(null, "Payment verification response missing critical data.");
                }

                return NewResult<PaystackPaymentVerificationResponse>.Failed(null, "Payment verification failed.");
            }
            catch (Exception ex)
            {

                logger.Error(ex, ex.Message);
                return NewResult<PaystackPaymentVerificationResponse>.Error(null, $"Error while verifying payment: {ex.Message}");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
