using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.External_Services.Requests
{
    public class FlutterwaveInitiateCardPaymentRequest
    {
        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("cvv")]
        public string CVV { get; set; }

        [JsonProperty("expiry_month")]
        public string ExpiryMonth { get; set; }

        [JsonProperty("expiry_year")]
        public string ExpiryYear { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("fullname")]
        public string FullName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("tx_ref")]
        public string TransactionReference { get; set; }

        [JsonProperty("redirect_url")]
        public string? RedirectUrl { get; set; } = "https://www.flutterwave.ng";
        [JsonProperty("authorization")]
        public Authorization Authorization { get; set; }
    }

    public class Authorization
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }
    }
}
