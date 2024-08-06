using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Service.Helpers.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using Serilog;


namespace Filip_Furniture.Service.Helpers.Implementations
{
    public class RestHelper : IRestHelper
    {


        private readonly ILogger _logger;

        public RestHelper(ILogger logger)
        {

            _logger = logger;

        }

        public async Task<T> DoWebRequestAsync<T>(MyBankLog log, string url, object request, string requestType, Dictionary<string, string> headers = null) where T : new()
        {
            var SDS = JsonConvert.SerializeObject(request);
            _logger.Information("URL: " + url + " " + JsonConvert.SerializeObject(request));
            T result = new T();

            Method method = requestType.ToLower() == "post" ? Method.Post : Method.Get;
            var client = new RestClient(url);
            var restRequest = new RestRequest(url, method);
            if (method == Method.Post)
            {
                restRequest.RequestFormat = DataFormat.Json;
                restRequest.AddJsonBody(request);

            }

            if (headers != null)
            {
                foreach (var item in headers)
                {
                    restRequest.AddHeader(item.Key, item.Value);
                }
            }

            try
            {
                RestResponse<T> response = await client.ExecuteAsync<T>(restRequest);

                _logger.Information("URL: " + url + " " + response.Content);
                if (!response.IsSuccessful)
                {
                    log.AdditionalInformation = $"URL: {url} {response.Content}";
                }

                result = JsonConvert.DeserializeObject<T>(response.Content);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                log.AdditionalInformation = $"URL: {url} {ex.Message}";

                return result;
            }
        }
    }
}