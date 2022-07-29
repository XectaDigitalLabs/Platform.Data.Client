# openapi_client.DailyProductionApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**production_add_daily**](DailyProductionApi.md#production_add_daily) | **PUT** /api/production/daily | 
[**production_delete_daily**](DailyProductionApi.md#production_delete_daily) | **DELETE** /api/production/daily/{xid} | 
[**production_get_daily**](DailyProductionApi.md#production_get_daily) | **GET** /api/production/daily/{uwi} | 


# **production_add_daily**
> [DailyProduction] production_add_daily(daily_production_input)



### Example

* Bearer (JWT) Authentication (bearerAuth):
```python
import time
import openapi_client
from openapi_client.api import daily_production_api
from openapi_client.model.daily_production import DailyProduction
from openapi_client.model.daily_production_input import DailyProductionInput
from pprint import pprint
# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = openapi_client.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure Bearer authorization (JWT): bearerAuth
configuration = openapi_client.Configuration(
    access_token = 'YOUR_BEARER_TOKEN'
)

# Enter a context with an instance of the API client
with openapi_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = daily_production_api.DailyProductionApi(api_client)
    daily_production_input = [
        DailyProductionInput(
            uwi="uwi_example",
            datetime=dateutil_parser('1970-01-01T00:00:00.00Z'),
            liquid_rate=3.14,
            oil_production_rate=3.14,
            gas_production_rate=3.14,
            water_production_rate=3.14,
            choke=3.14,
            gas_oil_ratio=3.14,
            water_cut=3.14,
            tubing_pressure=3.14,
            casing_pressure=3.14,
            gas_injection_rate=3.14,
            operating_frequency=3.14,
            strokes_per_minute=3.14,
            downtime_hours=3.14,
            downtime_code=1,
        ),
    ] # [DailyProductionInput] | 

    # example passing only required values which don't have defaults set
    try:
        api_response = api_instance.production_add_daily(daily_production_input)
        pprint(api_response)
    except openapi_client.ApiException as e:
        print("Exception when calling DailyProductionApi->production_add_daily: %s\n" % e)
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **daily_production_input** | [**[DailyProductionInput]**](DailyProductionInput.md)|  |

### Return type

[**[DailyProduction]**](DailyProduction.md)

### Authorization

[bearerAuth](../README.md#bearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **production_delete_daily**
> bool production_delete_daily(xid)



### Example

* Bearer (JWT) Authentication (bearerAuth):
```python
import time
import openapi_client
from openapi_client.api import daily_production_api
from pprint import pprint
# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = openapi_client.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure Bearer authorization (JWT): bearerAuth
configuration = openapi_client.Configuration(
    access_token = 'YOUR_BEARER_TOKEN'
)

# Enter a context with an instance of the API client
with openapi_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = daily_production_api.DailyProductionApi(api_client)
    xid = "xid_example" # str | 

    # example passing only required values which don't have defaults set
    try:
        api_response = api_instance.production_delete_daily(xid)
        pprint(api_response)
    except openapi_client.ApiException as e:
        print("Exception when calling DailyProductionApi->production_delete_daily: %s\n" % e)
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **xid** | **str**|  |

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
**0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **production_get_daily**
> [DailyProduction] production_get_daily(uwi)



### Example

* Bearer (JWT) Authentication (bearerAuth):
```python
import time
import openapi_client
from openapi_client.api import daily_production_api
from openapi_client.model.daily_production import DailyProduction
from pprint import pprint
# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = openapi_client.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure Bearer authorization (JWT): bearerAuth
configuration = openapi_client.Configuration(
    access_token = 'YOUR_BEARER_TOKEN'
)

# Enter a context with an instance of the API client
with openapi_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = daily_production_api.DailyProductionApi(api_client)
    uwi = "uwi_example" # str | 
    start_date = dateutil_parser('1000-01-01T00:00:00Z') # datetime |  (optional) if omitted the server will use the default value of dateutil_parser('1000-01-01T00:00:00Z')
    end_date = dateutil_parser('3000-12-31T00:00:00Z') # datetime |  (optional) if omitted the server will use the default value of dateutil_parser('3000-12-31T00:00:00Z')
    page = 1 # int |  (optional)
    limit = 1 # int |  (optional)

    # example passing only required values which don't have defaults set
    try:
        api_response = api_instance.production_get_daily(uwi)
        pprint(api_response)
    except openapi_client.ApiException as e:
        print("Exception when calling DailyProductionApi->production_get_daily: %s\n" % e)

    # example passing only required values which don't have defaults set
    # and optional values
    try:
        api_response = api_instance.production_get_daily(uwi, start_date=start_date, end_date=end_date, page=page, limit=limit)
        pprint(api_response)
    except openapi_client.ApiException as e:
        print("Exception when calling DailyProductionApi->production_get_daily: %s\n" % e)
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **uwi** | **str**|  |
 **start_date** | **datetime**|  | [optional] if omitted the server will use the default value of dateutil_parser('1000-01-01T00:00:00Z')
 **end_date** | **datetime**|  | [optional] if omitted the server will use the default value of dateutil_parser('3000-12-31T00:00:00Z')
 **page** | **int**|  | [optional]
 **limit** | **int**|  | [optional]

### Return type

[**[DailyProduction]**](DailyProduction.md)

### Authorization

[bearerAuth](../README.md#bearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

