# Org.OpenAPITools.Api.WellHeaderApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ProductionAddWellHeader**](WellHeaderApi.md#productionaddwellheader) | **PUT** /api/production/wellheader | 
[**ProductionAddWellHeaders**](WellHeaderApi.md#productionaddwellheaders) | **PUT** /api/production/wellheaders | 
[**ProductionDeleteWellHeader**](WellHeaderApi.md#productiondeletewellheader) | **DELETE** /api/production/wellheader/{uwi} | 
[**ProductionGetWellHeader**](WellHeaderApi.md#productiongetwellheader) | **GET** /api/production/wellheader/{uwi} | 
[**ProductionGetWellHeaders**](WellHeaderApi.md#productiongetwellheaders) | **GET** /api/production/wellheaders | 
[**ProductionUpdateWellHeader**](WellHeaderApi.md#productionupdatewellheader) | **PATCH** /api/production/wellheader | 


<a name="productionaddwellheader"></a>
# **ProductionAddWellHeader**
> WellHeader ProductionAddWellHeader (WellHeaderInput wellHeaderInput)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ProductionAddWellHeaderExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new WellHeaderApi(config);
            var wellHeaderInput = new WellHeaderInput(); // WellHeaderInput | 

            try
            {
                WellHeader result = apiInstance.ProductionAddWellHeader(wellHeaderInput);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WellHeaderApi.ProductionAddWellHeader: " + e.Message );
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
 **wellHeaderInput** | [**WellHeaderInput**](WellHeaderInput.md)|  | 

### Return type

[**WellHeader**](WellHeader.md)

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

<a name="productionaddwellheaders"></a>
# **ProductionAddWellHeaders**
> List&lt;WellHeader&gt; ProductionAddWellHeaders (List<WellHeaderInput> wellHeaderInput)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ProductionAddWellHeadersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new WellHeaderApi(config);
            var wellHeaderInput = new List<WellHeaderInput>(); // List<WellHeaderInput> | 

            try
            {
                List<WellHeader> result = apiInstance.ProductionAddWellHeaders(wellHeaderInput);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WellHeaderApi.ProductionAddWellHeaders: " + e.Message );
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
> bool ProductionDeleteWellHeader (string uwi)



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
            config.BasePath = "http://localhost";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new WellHeaderApi(config);
            var uwi = uwi_example;  // string | 

            try
            {
                bool result = apiInstance.ProductionDeleteWellHeader(uwi);
                Debug.WriteLine(result);
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

<a name="productiongetwellheader"></a>
# **ProductionGetWellHeader**
> WellHeader ProductionGetWellHeader (string uwi)



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
            config.BasePath = "http://localhost";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new WellHeaderApi(config);
            var uwi = uwi_example;  // string | 

            try
            {
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
            config.BasePath = "http://localhost";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new WellHeaderApi(config);
            var offset = 56;  // int? |  (optional) 
            var limit = 56;  // int? |  (optional) 

            try
            {
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

<a name="productionupdatewellheader"></a>
# **ProductionUpdateWellHeader**
> WellHeader ProductionUpdateWellHeader (WellHeader wellHeader)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ProductionUpdateWellHeaderExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure Bearer token for authorization: bearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new WellHeaderApi(config);
            var wellHeader = new WellHeader(); // WellHeader | 

            try
            {
                WellHeader result = apiInstance.ProductionUpdateWellHeader(wellHeader);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WellHeaderApi.ProductionUpdateWellHeader: " + e.Message );
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
 **wellHeader** | [**WellHeader**](WellHeader.md)|  | 

### Return type

[**WellHeader**](WellHeader.md)

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

