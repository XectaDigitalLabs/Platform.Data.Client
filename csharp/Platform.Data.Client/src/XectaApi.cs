using System;
using System.Net;
using System.Security.Authentication;
using System.Text;
using Org.OpenAPITools.Client;
using Platform.Data.Client;

namespace Data.Platform.Client.CSharp
{
    public class XectaApi
    {
        private Configuration _configuration;

        public XectaApi(string baseUrl = "https://testawsapi.onxecta.com", string clientCertFile = null,
            string clientKeyFile = null)
        {
            _configuration = new Configuration
            {
                BasePath = baseUrl
            };
            _configuration.ClientCertificates = MTLS._x509CertificateCollection(clientCertFile, clientKeyFile);
        }

        public XectaApiClient Authenticate(string apiKey, string apiSecret)
        {
            var base64Auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"));
            var client = new ApiClient(_configuration.BasePath);
            var requestOptions = new RequestOptions()
            {
                QueryParameters = new Multimap<string, string>
                {
                    { "grant_type", "client_credentials" }
                },
                HeaderParameters = new Multimap<string, string>
                {
                    { "Content-Type", "application/x-www-form-urlencoded" },
                    { "Authorization", $"Basic {base64Auth}" }
                }
            };

            var response = client.Post<AuthResponse>("/authenticate/oauth2/token", requestOptions, _configuration);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new AuthenticationException(
                    $"Response from Authentication API was {response.StatusCode} with error: {response.Data}");
            }

            var authResponse = response.Data;
            
            // TODO check http return codes
            return new XectaApiClient(_configuration, authResponse.AccessToken);
        }
    }
}