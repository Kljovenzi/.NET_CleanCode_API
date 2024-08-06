using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.External_Services.Requests
{
    public class PaystackPaymentInitiationRequest
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
    }
}
