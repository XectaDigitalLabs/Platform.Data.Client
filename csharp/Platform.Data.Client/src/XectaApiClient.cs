using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;

namespace Data.Platform.Client.CSharp
{
    public class XectaApiClient : ApiClient
    {
        private readonly Configuration _configuration;
        private readonly ApiClient _client;
        private readonly ApiClient _asyncClient;

        public XectaApiClient(Configuration configuration, string bearerToken)
        {
            _configuration = configuration;
            _configuration.AccessToken = bearerToken;
            
            _client = new ApiClient(_configuration.BasePath);
            _asyncClient = new ApiClient(_configuration.BasePath);
        }

        public DailyProductionApi ProductionApi()
        {
            return new DailyProductionApi(_client, _asyncClient, _configuration);
        }
        public WellHeaderApi WellHeaderApi()
        {
            return new WellHeaderApi(_client, _asyncClient, _configuration);
        }
    }
}