using Filip_Furniture.Domain.Common.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Interfaces
{
    public interface IPushNotificationService
    {
        Task<NewResult<string>> SendPushNotificationAsync(string userId, string body);
    }
}
