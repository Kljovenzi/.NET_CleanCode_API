using Asp.Versioning;
using Filip_Furniture.Service.External_Services.Requests;
using Filip_Furniture.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Filip_Furniture.API.Controllers.v1
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/initiate-paystack-payment")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> InitiatePaystackPayment(PaystackPaymentInitiationRequest request)
        {

            var response = await paymentService.InitiatePayment(request.Amount, request.Email);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpGet("api/v{version:apiVersion}/[controller]/verify-paystack-payment")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> VerifyPaystackPayment([FromQuery] PaystackPaymentVerificationRequest request)
        {
            var response = await paymentService.VerifyPayment(request.Reference);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/initiate-flutterwave-card-payment")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> InitiateFlutterwavePayment([FromBody] FlutterwaveInitiateCardPaymentRequest request)
        {
            var response = await paymentService.InitiateFlutterwaveCardPayment(request);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/flutterwave-validate-charge")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> FlutterwaveValidateCharge([FromBody] FlutterwaveValidateChargeRequest request)
        {
            var response = await paymentService.FlutterwaveValidateCharge(request);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpGet("api/v{version:apiVersion}/[controller]/flutterwave-get-payment-status")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> FlutterwaveVerifyPayment([FromQuery] FlutterwaveVerifyCardPaymentRequest request)
        {
            var response = await paymentService.FlutterwaveVerifyCardPayment(request);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }
    }
}
