using Filip_Furniture.Data.Repositories.Interfaces;
using Filip_Furniture.Domain.Common.Generics;
using Filip_Furniture.Service.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Filip_Furniture.Service.Implementations
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IUserRepostiory userRepository;
        public PushNotificationService(IUserRepostiory userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<NewResult<string>> SendPushNotificationAsync(string userId, string body)
        {
            try
            {
                var user = await userRepository.GetUserById(userId);
                //if (user == null || string.IsNullOrEmpty(user.DeviceToken))
                //    throw new ArgumentNullException(nameof(user.DeviceToken), "User device token cannot be null or empty.");

                var message = new Message
                {
                    Token = GenerateMockDeviceToken(),
                    Notification = new Notification
                    {
                        Title = "Order Update",
                        Body = body
                    }
                };

                // Ensure Firebase is initialized
                if (FirebaseApp.DefaultInstance == null)
                {
                    FirebaseApp.Create(new AppOptions
                    {
                        Credential = GoogleCredential.FromFile("Properties/NotificationFile.json")
                    });
                }

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                return NewResult<string>.Success(response, "Push notification sent successfully.");
            }
            catch (Exception ex)
            {
                return NewResult<string>.Failed(null, $"Error occurred: {ex.Message}");
            }
        }

        public string GenerateMockDeviceToken()
        {
            // Define the characters allowed in the device token
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            // Define the length of the device token
            const int tokenLength = 140;

            // Use a StringBuilder to construct the device token
            StringBuilder tokenBuilder = new StringBuilder();

            // Use a random number generator to select characters from the allowed set
            Random random = new Random();
            for (int i = 0; i < tokenLength; i++)
            {
                int index = random.Next(allowedChars.Length);
                tokenBuilder.Append(allowedChars[index]);
            }

            // Return the generated device token
            return tokenBuilder.ToString();
        }

    }
}
