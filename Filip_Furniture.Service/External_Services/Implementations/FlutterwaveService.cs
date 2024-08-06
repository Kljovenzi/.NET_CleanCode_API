using Filip_Furniture.Service.External_Services.Interfaces;
using Filip_Furniture.Service.Helpers.Interfaces;
using System.Net;
using Serilog;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Filip_Furniture.Service.External_Services.Responses.Flutterwave.VerifyPaymentResponse;
using Filip_Furniture.Service.External_Services.Requests;
using Filip_Furniture.Service.External_Services.Responses.Flutterwave.VerifyResponse;
using Filip_Furniture.Domain.Entities;
using Authorization = Filip_Furniture.Service.External_Services.Requests.Authorization;
using Filip_Furniture.Service.External_Services.Responses.Flutterwave;
using System.Security.Cryptography;

namespace Filip_Furniture.Service.External_Services.Implementations
{
    public class FlutterwaveService : IFlutterwaveService
    {
        private readonly IRestHelper restHelper;
        private readonly ILogger logger;
        private readonly string encryptionKey;
        private readonly TripleDES tripleDes;
        public FlutterwaveService(IRestHelper restHelper, ILogger logger, TripleDES tripleDes)
        {
            this.restHelper = restHelper;
            this.logger = logger;
            this.tripleDes = tripleDes;
            this.encryptionKey = Environment.GetEnvironmentVariable("FlutterwaveEncryptionKey");
        }
        public async Task<FlutterwaveInitiateCardPaymentResponse> InitiatePaymentAsync(FlutterwaveInitiateCardPaymentRequest request, MyBankLog log)
        {
            try
            {
                logger.Information("Initiate payment service");
                string flutterwaveApiUrl = Environment.GetEnvironmentVariable("InitiateFlutterwaveApiUrl");
                string flutterwaveSecretKey = Environment.GetEnvironmentVariable("FlutterwaveSecretKey");
                var flutterwaveRequest = new FlutterwaveInitiateCardPaymentRequest
                {
                    CardNumber = request.CardNumber,
                    CVV = request.CVV,
                    ExpiryMonth = request.ExpiryMonth,
                    ExpiryYear = request.ExpiryYear,
                    Currency = request.Currency,
                    Amount = request.Amount,
                    FullName = request.FullName,
                    Email = request.Email,
                    TransactionReference = request.TransactionReference,
                    RedirectUrl = "https://www.flutterwave.ng",
                    Authorization = new Authorization
                    {
                        Mode = request.Authorization.Mode,
                        City = request.Authorization.City,
                        Address = request.Authorization.Address,
                        State = request.Authorization.State,
                        Country = request.Authorization.Country,
                        Zipcode = request.Authorization.Zipcode
                    }
                };
                var headers = new Dictionary<string, string>
                {
                    { "Authorization", $"Bearer {flutterwaveSecretKey}" },

                };
                // Serialize the request to JSON
                string requestJson = JsonConvert.SerializeObject(flutterwaveRequest);
                // Encrypt the JSON payload using 3DES
                string encryptedPayload = requestJson + encryptionKey;

                var initiationResponse = await restHelper.DoWebRequestAsync<FlutterwaveInitiateCardPaymentResponse>(log,
                                                                                                       flutterwaveApiUrl,
                                                                                                       encryptedPayload,
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

        public async Task<FlutterwaveValidateChargeResponse> ValidateChargeAsync(FlutterwaveValidateChargeRequest request, MyBankLog log)
        {
            try
            {
                logger.Information("Validate charge service");
                string flutterwaveApiUrl = Environment.GetEnvironmentVariable("FlutterwaveValidateChargeApiUrl");
                string flutterwaveSecretKey = Environment.GetEnvironmentVariable("FlutterwaveSecretKey");
                var flutterwaveRequest = new FlutterwaveValidateChargeRequest
                {
                    Otp = request.Otp,
                    Flw_ref = request.Flw_ref
                };
                var headers = new Dictionary<string, string>
                {
                    { "Authorization", $"Bearer {flutterwaveSecretKey}" },

                };
                var verifyResponse = await restHelper.DoWebRequestAsync<FlutterwaveValidateChargeResponse>(log,
                                                                                                       flutterwaveApiUrl,
                                                                                                       flutterwaveRequest,
                                                                                                       "post",
                                                                                                       headers);
                return verifyResponse;

            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                log.AdditionalInformation = ex.Message;
                return null;
            }
        }

        public async Task<FlutterwaveVerifyCardPaymentResponse> VerifyPaymentAsync(FlutterwaveVerifyCardPaymentRequest request, MyBankLog log)
        {
            try
            {
                logger.Information("Validate charge service");
                string flutterwaveApiUrl = Environment.GetEnvironmentVariable("VerifyCardPaymentFlutterwaveApiUrl") + $"/{request.TransactionId}" + "/verify";
                string flutterwaveSecretKey = Environment.GetEnvironmentVariable("FlutterwaveSecretKey");
                var headers = new Dictionary<string, string>
                {
                    { "Authorization", $"Bearer {flutterwaveSecretKey}" },

                };
                var verifyResponse = await restHelper.DoWebRequestAsync<FlutterwaveVerifyCardPaymentResponse>(log,
                                                                                                              flutterwaveApiUrl,
                                                                                                              null,
                                                                                                              "get",
                                                                                                              headers);
                return verifyResponse;
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
