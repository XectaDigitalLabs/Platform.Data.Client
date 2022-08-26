# Org.OpenAPITools.Api.DailyProductionApi

All URIs are relative to *https://data-sandbox.onxecta.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ProductionAddUpdateDaily**](DailyProductionApi.md#productionaddupdatedaily) | **POST** /api/production/daily | Bulk Add / Update Daily Production Data
[**ProductionDeleteDaily**](DailyProductionApi.md#productiondeletedaily) | **DELETE** /api/production/daily/{xid} | Delete Daily Production Record
[**ProductionGetDaily**](DailyProductionApi.md#productiongetdaily) | **GET** /api/production/daily/{uwi} | Fetch Daily Production Records


<a name="productionaddupdatedaily"></a>
# **ProductionAddUpdateDaily**
> List&lt;DailyProduction&gt; ProductionAddUpdateDaily (List<DailyProductionInput> dailyProductionInput)

Bulk Add / Update Daily Production Data

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ProductionAddUpdateDailyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://data-sandbox.onxecta.com";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DailyProductionApi(config);
            var dailyProductionInput = new List<DailyProductionInput>(); // List<DailyProductionInput> | 

            try
            {
                // Bulk Add / Update Daily Production Data
                List<DailyProduction> result = apiInstance.ProductionAddUpdateDaily(dailyProductionInput);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DailyProductionApi.ProductionAddUpdateDaily: " + e.Message );
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
 **dailyProductionInput** | [**List&lt;DailyProductionInput&gt;**](DailyProductionInput.md)|  | 

### Return type

[**List&lt;DailyProduction&gt;**](DailyProduction.md)

### Authorization

[bearerAuth](../README.md#bearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/x-ndjson

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="productiondeletedaily"></a>
# **ProductionDeleteDaily**
> void ProductionDeleteDaily (string xid)

Delete Daily Production Record

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ProductionDeleteDailyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://data-sandbox.onxecta.com";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DailyProductionApi(config);
            var xid = xid_example;  // string | 

            try
            {
                // Delete Daily Production Record
                apiInstance.ProductionDeleteDaily(xid);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DailyProductionApi.ProductionDeleteDaily: " + e.Message );
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
 **xid** | **string**|  | 

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

<a name="productiongetdaily"></a>
# **ProductionGetDaily**
> List&lt;DailyProduction&gt; ProductionGetDaily (string uwi, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? limit = null)

Fetch Daily Production Records

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ProductionGetDailyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://data-sandbox.onxecta.com";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DailyProductionApi(config);
            var uwi = uwi_example;  // string | 
            var startDate = 2013-10-20T19:20:30+01:00;  // DateTime? |  (optional) 
            var endDate = 2013-10-20T19:20:30+01:00;  // DateTime? |  (optional) 
            var page = 56;  // int? |  (optional) 
            var limit = 56;  // int? |  (optional) 

            try
            {
                // Fetch Daily Production Records
                List<DailyProduction> result = apiInstance.ProductionGetDaily(uwi, startDate, endDate, page, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DailyProductionApi.ProductionGetDaily: " + e.Message );
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
 **startDate** | **DateTime?**|  | [optional] 
 **endDate** | **DateTime?**|  | [optional] 
 **page** | **int?**|  | [optional] 
 **limit** | **int?**|  | [optional] 

### Return type

[**List&lt;DailyProduction&gt;**](DailyProduction.md)

### Authorization

[bearerAuth](../README.md#bearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/x-ndjson

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

