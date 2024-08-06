using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.External_Services.Requests
{
    public class FlutterwaveValidateChargeRequest
    {
        [JsonProperty("otp")]
        public string Otp { get; set; }

        [JsonProperty("flw_ref")]
        public string Flw_ref { get; set; }
    }
}
