# Org.OpenAPITools.Api.WellHeaderApi

All URIs are relative to *https://data-sandbox.onxecta.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ProductionAddUpdateWellHeaders**](WellHeaderApi.md#productionaddupdatewellheaders) | **POST** /api/production/wellheaders | Bulk Add / Update WellHeader Data
[**ProductionDeleteWellHeader**](WellHeaderApi.md#productiondeletewellheader) | **DELETE** /api/production/wellheader/{uwi} | Delete Single Well Header
[**ProductionGetWellHeader**](WellHeaderApi.md#productiongetwellheader) | **GET** /api/production/wellheader/{uwi} | Fetch Single Well Header
[**ProductionGetWellHeaders**](WellHeaderApi.md#productiongetwellheaders) | **GET** /api/production/wellheaders | Fetch Multiple Well Headers


<a name="productionaddupdatewellheaders"></a>
# **ProductionAddUpdateWellHeaders**
> List&lt;WellHeader&gt; ProductionAddUpdateWellHeaders (List<WellHeaderInput> wellHeaderInput)

Bulk Add / Update WellHeader Data

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ProductionAddUpdateWellHeadersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://data-sandbox.onxecta.com";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new WellHeaderApi(config);
            var wellHeaderInput = new List<WellHeaderInput>(); // List<WellHeaderInput> | 

            try
            {
                // Bulk Add / Update WellHeader Data
                List<WellHeader> result = apiInstance.ProductionAddUpdateWellHeaders(wellHeaderInput);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WellHeaderApi.ProductionAddUpdateWellHeaders: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **wellHeaderInput** | [**List&lt;WellHeaderInput&gt;**](WellHeaderInput.md)|  | 

### Return type

[**List&lt;WellHeader&gt;**](WellHeader.md)

### Authorization

[bearerAuth](../README.md#bearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="productiondeletewellheader"></a>
# **ProductionDeleteWellHeader**
> void ProductionDeleteWellHeader (string uwi)

Delete Single Well Header

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ProductionDeleteWellHeaderExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://data-sandbox.onxecta.com";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new WellHeaderApi(config);
            var uwi = uwi_example;  // string | 

            try
            {
                // Delete Single Well Header
                apiInstance.ProductionDeleteWellHeader(uwi);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WellHeaderApi.ProductionDeleteWellHeader: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **uwi** | **string**|  | 

### Return type

void (empty response body)

### Authorization

[bearerAuth](../README.md#bearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="productiongetwellheader"></a>
# **ProductionGetWellHeader**
> WellHeader ProductionGetWellHeader (string uwi)

Fetch Single Well Header

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ProductionGetWellHeaderExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://data-sandbox.onxecta.com";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new WellHeaderApi(config);
            var uwi = uwi_example;  // string | 

            try
            {
                // Fetch Single Well Header
                WellHeader result = apiInstance.ProductionGetWellHeader(uwi);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WellHeaderApi.ProductionGetWellHeader: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **uwi** | **string**|  | 

### Return type

[**WellHeader**](WellHeader.md)

### Authorization

[bearerAuth](../README.md#bearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="productiongetwellheaders"></a>
# **ProductionGetWellHeaders**
> List&lt;WellHeader&gt; ProductionGetWellHeaders (int? offset = null, int? limit = null)

Fetch Multiple Well Headers

This operation offers an optional paging facility if desired.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ProductionGetWellHeadersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://data-sandbox.onxecta.com";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new WellHeaderApi(config);
            var offset = 56;  // int? |  (optional) 
            var limit = 56;  // int? |  (optional) 

            try
            {
                // Fetch Multiple Well Headers
                List<WellHeader> result = apiInstance.ProductionGetWellHeaders(offset, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WellHeaderApi.ProductionGetWellHeaders: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **offset** | **int?**|  | [optional] 
 **limit** | **int?**|  | [optional] 

### Return type

[**List&lt;WellHeader&gt;**](WellHeader.md)

### Authorization

[bearerAuth](../README.md#bearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

