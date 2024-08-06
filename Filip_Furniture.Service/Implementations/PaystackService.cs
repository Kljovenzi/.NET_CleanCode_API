using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Service.External_Services.Requests;
using Filip_Furniture.Service.External_Services.Responses;
using Filip_Furniture.Service.External_Services.Responses.Verification;
using Filip_Furniture.Service.Helpers.Interfaces;
using Filip_Furniture.Service.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Implementations
{
    public class PaystackService : IPaystackService
    {
        // private readonly IConfiguration config;
        private readonly IRestHelper restHelper;
        private readonly ILogger logger;
        public PaystackService(IRestHelper restHelper, ILogger logger)
        {
            //  this.config = config;
            this.restHelper = restHelper;
            this.logger = logger;
        }
        public async Task<PaystackPaymentInitiationResponse> InitiatePaymentAsync(PaystackPaymentInitiationRequest request, MyBankLog log)
        {
            try
            {
                logger.Information("Initiate payment service");
                string paystackApiUrl = Environment.GetEnvironmentVariable("InitiatePaymentApiUrl");
                string paystackSecretKey = Environment.GetEnvironmentVariable("SecretKey");
                var paystackRequest = new PaystackPaymentInitiationRequest
                {
                    Amount = request.Amount,
                    Email = request.Email,
                };
                var headers = new Dictionary<string, string>
                {
                    { "Authorization", $"Bearer {paystackSecretKey}" },

                };
                var initiationResponse = await restHelper.DoWebRequestAsync<PaystackPaymentInitiationResponse>(log,
                                                                                                        paystackApiUrl,
                                                                                                        paystackRequest,
                                                                                                        "post",
                                                                                                        headers);
                return initiationResponse;
            }
            catch (Exception ex)
            {

                logger.Error(ex, ex.Message);
                log.AdditionalInformation = ex.Message;
                return null;
            }
        }

        public async Task<PaystackPaymentVerificationResponse> VerifyPaymentAsync(PaystackPaymentVerificationRequest request, MyBankLog log)
        {
            try
            {
                logger.Information("Verify payment service");
                string paystackApiUrl = Environment.GetEnvironmentVariable("VerifyReferenceApiUrl") + $"/{request.Reference}";
                string paystackSecretKey = Environment.GetEnvironmentVariable("SecretKey");
                var paystackVerificationRequest = new PaystackPaymentVerificationRequest
                {
                    Reference = request.Reference,
                };
                var headers = new Dictionary<string, string>
                {
                    { "Authorization", $"Bearer {paystackSecretKey}" },

                };
                var verificationResponse = await restHelper.DoWebRequestAsync<PaystackPaymentVerificationResponse>(log,
                                                                                                                   paystackApiUrl,
                                                                                                                   null,
                                                                                                                   "get",
                                                                                                                   headers);
                return verificationResponse;
            }
            catch (Exception ex)
            {


                logger.Error(ex, ex.Message);
                log.AdditionalInformation = ex.Message;
                return null;
            }
        }
    }
}
