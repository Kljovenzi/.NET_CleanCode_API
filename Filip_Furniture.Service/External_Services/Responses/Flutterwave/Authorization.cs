using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.External_Services.Responses.Flutterwave
{
    public class Authorization
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }
    }

    public class Data
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("tx_ref")]
        public string TxRef { get; set; }

        [JsonProperty("flw_ref")]
        public string FlwRef { get; set; }

        [JsonProperty("processor_response")]
        public string ProcessorResponse { get; set; }
    }

    public class Meta
    {
        [JsonProperty("authorization")]
        public Authorization Authorization { get; set; }
    }

    public class FlutterwaveInitiateCardPaymentResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
