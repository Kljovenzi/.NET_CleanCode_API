using Filip_Furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Helpers.Interfaces
{
    public interface IRestHelper
    {
        Task<T> DoWebRequestAsync<T>(MyBankLog log, string url, object request, string requestType, Dictionary<string, string> headers = null) where T : new();

    }
}
