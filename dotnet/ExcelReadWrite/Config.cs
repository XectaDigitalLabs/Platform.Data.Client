
/// <summary>
/// Holds the config info
/// </summary>
public class Config
{
    /// <summary>
    /// Url of the API 
    /// </summary>
    public string ApiUrl { get; set; } = "https://data.onxecta.com";
    /// <summary>
    /// Url of the Auth Services
    /// </summary>
    public string AuthUrl { get; set; } = "https://prod.authenticate.onxecta.com";
    /// <summary>
    /// Api Client Id
    /// </summary>
    public string ApiClientId { get; set; } = "<Your client Id>";
    /// <summary>
    /// Api Key
    /// </summary>
    public string ApiClientSecret { get; set; } = "<Your client Secret>";
    /// <summary>
    /// Location of TLS PEM File
    /// </summary>
    public string TlsPem { get; set; } = @"c:\demo_keys\xecta-data-api.pem";
    /// <summary>
    /// Location of TLS Key File
    /// </summary>
    public string TlsKey { get; set; } = @"c:\demo_keys\xecta-data-api.key";
}