using Asp.Versioning;
using Filip_Furniture.Domain.DataTransferObjects;
using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Service.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Filip_Furniture.API.Controllers.v1
{
    public class AuthenticationController : BaseController
    {
        private readonly IUserService userService;
        public AuthenticationController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/user/register")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> RegisterUser([FromBody] User request)
        {

            var response = await userService.RegisterUser(request);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/admin/register")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Admin request)
        {

            var response = await userService.RegisterAdmin(request);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/user/login")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> UserLogin([FromBody] Domain.DataTransferObjects.LoginRequest request)
        {

            var response = await userService.UserLogin(request);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };

        }

        [HttpPost("api/v{version:apiVersion}/[controller]/admin/login")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginRequest request)
        {

            var response = await userService.AdminLogin(request);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };

        }

        [HttpPost("api/v{version:apiVersion}/[controller]/user/reset-password")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> ResetPassword([FromBody] Domain.DataTransferObjects.ResetPasswordRequest request)
        {

            var response = await userService.ResetPassword(request.EmailAddress, request.VerificationCode, request.NewPassword);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };

        }

        [HttpPost("api/v{version:apiVersion}/[controller]/user/activate-user-account")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> ActivateUserAccount([FromBody] ActivateAccountRequest request)
        {

            var response = await userService.ActivateAccount(request.EmailAddress, request.ActivationCode);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };

        }

        [HttpPost("api/v{version:apiVersion}/[controller]/admin/activate-admin-account")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> ActivateAdminAccount([FromBody] ActivateAccountRequest request)
        {

            var response = await userService.ActivateAdminAccount(request.EmailAddress, request.ActivationCode);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };

        }

        [HttpPost("api/v{version:apiVersion}/[controller]/resend-verification-code")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> ResendVerificationCode(string EmailAddress)
        {

            var response = await userService.ResendVerificationCode(EmailAddress);

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

