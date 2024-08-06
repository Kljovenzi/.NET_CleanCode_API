using Asp.Versioning;
using Filip_Furniture.Domain.DataTransferObjects;
using Filip_Furniture.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Filip_Furniture.API.Controllers.v1
{
    public class PushNotificationController : BaseController
    {
        private readonly IPushNotificationService pushNotificationService;
        public PushNotificationController(IPushNotificationService pushNotificationService)
        {
            this.pushNotificationService = pushNotificationService;
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/send-push-notification")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> SendPushNotification([FromBody] PushNotificationRequest request)
        {
            var response = await pushNotificationService.SendPushNotificationAsync(request.UserId, request.body);

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
