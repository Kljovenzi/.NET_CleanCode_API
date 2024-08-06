using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Filip_Furniture.API
{
    public static class FirebaseInitializer
    {
        public static void Initialize()
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile("appsettings.json/appsettings.Development.json")
            });
        }
    }
}
