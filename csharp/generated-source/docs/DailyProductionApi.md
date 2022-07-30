# Org.OpenAPITools.Api.DailyProductionApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ProductionAddDaily**](DailyProductionApi.md#productionadddaily) | **PUT** /api/production/daily | 
[**ProductionDeleteDaily**](DailyProductionApi.md#productiondeletedaily) | **DELETE** /api/production/daily/{xid} | 
[**ProductionGetDaily**](DailyProductionApi.md#productiongetdaily) | **GET** /api/production/daily/{uwi} | 


<a name="productionadddaily"></a>
# **ProductionAddDaily**
> List&lt;DailyProduction&gt; ProductionAddDaily (List<DailyProductionInput> dailyProductionInput)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ProductionAddDailyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DailyProductionApi(config);
            var dailyProductionInput = new List<DailyProductionInput>(); // List<DailyProductionInput> | 

            try
            {
                List<DailyProduction> result = apiInstance.ProductionAddDaily(dailyProductionInput);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DailyProductionApi.ProductionAddDaily: " + e.Message );
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
 - **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="productiondeletedaily"></a>
# **ProductionDeleteDaily**
> bool ProductionDeleteDaily (string xid)



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
            config.BasePath = "http://localhost";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DailyProductionApi(config);
            var xid = xid_example;  // string | 

            try
            {
                bool result = apiInstance.ProductionDeleteDaily(xid);
                Debug.WriteLine(result);
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

**bool**

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

<a name="productiongetdaily"></a>
# **ProductionGetDaily**
> List&lt;DailyProduction&gt; ProductionGetDaily (string uwi, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? limit = null)



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
            config.BasePath = "http://localhost";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DailyProductionApi(config);
            var uwi = uwi_example;  // string | 
            var startDate = 2013-10-20T19:20:30+01:00;  // DateTime? |  (optional)  (default to "1000-01-01T00:00Z")
            var endDate = 2013-10-20T19:20:30+01:00;  // DateTime? |  (optional)  (default to "3000-12-31T00:00Z")
            var page = 56;  // int? |  (optional) 
            var limit = 56;  // int? |  (optional) 

            try
            {
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
 **startDate** | **DateTime?**|  | [optional] [default to &quot;1000-01-01T00:00Z&quot;]
 **endDate** | **DateTime?**|  | [optional] [default to &quot;3000-12-31T00:00Z&quot;]
 **page** | **int?**|  | [optional] 
 **limit** | **int?**|  | [optional] 

### Return type

[**List&lt;DailyProduction&gt;**](DailyProduction.md)

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

