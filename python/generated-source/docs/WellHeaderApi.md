# openapi_client.WellHeaderApi

All URIs are relative to *https://data-sandbox.onxecta.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**production_add_update_well_headers**](WellHeaderApi.md#production_add_update_well_headers) | **POST** /api/production/wellheaders | Bulk Add / Update WellHeader Data
[**production_delete_well_header**](WellHeaderApi.md#production_delete_well_header) | **DELETE** /api/production/wellheader/{uwi} | Delete Single Well Header
[**production_get_well_header**](WellHeaderApi.md#production_get_well_header) | **GET** /api/production/wellheader/{uwi} | Fetch Single Well Header
[**production_get_well_headers**](WellHeaderApi.md#production_get_well_headers) | **GET** /api/production/wellheaders | Fetch Multiple Well Headers


# **production_add_update_well_headers**
> [WellHeader] production_add_update_well_headers(well_header_input)

Bulk Add / Update WellHeader Data

### Example

* Bearer (JWT) Authentication (bearerAuth):
```python
import time
import openapi_client
from openapi_client.api import well_header_api
from openapi_client.model.well_header_input import WellHeaderInput
from openapi_client.model.well_header import WellHeader
from pprint import pprint
# Defining the host is optional and defaults to https://data-sandbox.onxecta.com
# See configuration.py for a list of all supported configuration parameters.
configuration = openapi_client.Configuration(
    host = "https://data-sandbox.onxecta.com"
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
    api_instance = well_header_api.WellHeaderApi(api_client)
    well_header_input = [
        WellHeaderInput(
            uwi="uwi_example",
            name="name_example",
            group="group_example",
            field="field_example",
            route="route_example",
            type="PRODUCER",
            fluid="OIL",
            lift_type="NATURAL_FLOW",
            lat=3.14,
            lon=3.14,
        ),
    ] # [WellHeaderInput] | 

    # example passing only required values which don't have defaults set
    try:
        # Bulk Add / Update WellHeader Data
        api_response = api_instance.production_add_update_well_headers(well_header_input)
        pprint(api_response)
    except openapi_client.ApiException as e:
        print("Exception when calling WellHeaderApi->production_add_update_well_headers: %s\n" % e)
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **well_header_input** | [**[WellHeaderInput]**](WellHeaderInput.md)|  |

### Return type

[**[WellHeader]**](WellHeader.md)

### Authorization

[bearerAuth](../README.md#bearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
**0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **production_delete_well_header**
> production_delete_well_header(uwi)

Delete Single Well Header

### Example

* Bearer (JWT) Authentication (bearerAuth):
```python
import time
import openapi_client
from openapi_client.api import well_header_api
from pprint import pprint
# Defining the host is optional and defaults to https://data-sandbox.onxecta.com
# See configuration.py for a list of all supported configuration parameters.
configuration = openapi_client.Configuration(
    host = "https://data-sandbox.onxecta.com"
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
    api_instance = well_header_api.WellHeaderApi(api_client)
    uwi = "uwi_example" # str | 

    # example passing only required values which don't have defaults set
    try:
        # Delete Single Well Header
        api_instance.production_delete_well_header(uwi)
    except openapi_client.ApiException as e:
        print("Exception when calling WellHeaderApi->production_delete_well_header: %s\n" % e)
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **uwi** | **str**|  |

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
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **production_get_well_header**
> WellHeader production_get_well_header(uwi)

Fetch Single Well Header

### Example

* Bearer (JWT) Authentication (bearerAuth):
```python
import time
import openapi_client
from openapi_client.api import well_header_api
from openapi_client.model.well_header import WellHeader
from pprint import pprint
# Defining the host is optional and defaults to https://data-sandbox.onxecta.com
# See configuration.py for a list of all supported configuration parameters.
configuration = openapi_client.Configuration(
    host = "https://data-sandbox.onxecta.com"
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
    api_instance = well_header_api.WellHeaderApi(api_client)
    uwi = "uwi_example" # str | 

    # example passing only required values which don't have defaults set
    try:
        # Fetch Single Well Header
        api_response = api_instance.production_get_well_header(uwi)
        pprint(api_response)
    except openapi_client.ApiException as e:
        print("Exception when calling WellHeaderApi->production_get_well_header: %s\n" % e)
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **uwi** | **str**|  |

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
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **production_get_well_headers**
> [WellHeader] production_get_well_headers()

Fetch Multiple Well Headers

This operation offers an optional paging facility if desired.

### Example

* Bearer (JWT) Authentication (bearerAuth):
```python
import time
import openapi_client
from openapi_client.api import well_header_api
from openapi_client.model.well_header import WellHeader
from pprint import pprint
# Defining the host is optional and defaults to https://data-sandbox.onxecta.com
# See configuration.py for a list of all supported configuration parameters.
configuration = openapi_client.Configuration(
    host = "https://data-sandbox.onxecta.com"
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
    api_instance = well_header_api.WellHeaderApi(api_client)
    offset = 1 # int |  (optional)
    limit = 1 # int |  (optional)

    # example passing only required values which don't have defaults set
    # and optional values
    try:
        # Fetch Multiple Well Headers
        api_response = api_instance.production_get_well_headers(offset=offset, limit=limit)
        pprint(api_response)
    except openapi_client.ApiException as e:
        print("Exception when calling WellHeaderApi->production_get_well_headers: %s\n" % e)
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **offset** | **int**|  | [optional]
 **limit** | **int**|  | [optional]

### Return type

[**[WellHeader]**](WellHeader.md)

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

