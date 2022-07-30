using Newtonsoft.Json;

namespace Data.Platform.Client.CSharp
{
    /// <summary>
    /// Xecta response object that contains the bearer token, with expiration.
    /// </summary>
    public record AuthResponse
    {
        [JsonProperty("access_token")] public string AccessToken { get; set; }
        [JsonProperty("token_type")] public string TokenType { get; set; }
        [JsonProperty("expires_in")] public string ExpiresIn { get; set; }
    }
}