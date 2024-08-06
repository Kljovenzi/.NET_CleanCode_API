using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.External_Services.Responses.Flutterwave.VerifyResponse
{
    public class Data
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("tx_ref")]
        public string TxRef { get; set; }

        [JsonProperty("flw_ref")]
        public string FlwRef { get; set; }
    }

    public class FlutterwaveValidateChargeResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
