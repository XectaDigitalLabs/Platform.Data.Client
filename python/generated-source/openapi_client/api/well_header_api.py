"""
    Production API

    API exposing endpoints for managing well headers and daily production.  # noqa: E501

    The version of the OpenAPI document: 1.0
    Generated by: https://openapi-generator.tech
"""


import re  # noqa: F401
import sys  # noqa: F401

from openapi_client.api_client import ApiClient, Endpoint
from openapi_client.model_utils import (  # noqa: F401
    check_allowed_values,
    check_validations,
    date,
    datetime,
    file_type,
    none_type,
    validate_and_convert_types
)
from openapi_client.model.well_header import WellHeader
from openapi_client.model.well_header_input import WellHeaderInput


class WellHeaderApi(object):
    """NOTE: This class is auto generated by OpenAPI Generator
    Ref: https://openapi-generator.tech

    Do not edit the class manually.
    """

    def __init__(self, api_client=None):
        if api_client is None:
            api_client = ApiClient()
        self.api_client = api_client

        def __production_add_well_header(
            self,
            well_header_input,
            **kwargs
        ):
            """production_add_well_header  # noqa: E501

            This method makes a synchronous HTTP request by default. To make an
            asynchronous HTTP request, please pass async_req=True

            >>> thread = api.production_add_well_header(well_header_input, async_req=True)
            >>> result = thread.get()

            Args:
                well_header_input (WellHeaderInput):

            Keyword Args:
                _return_http_data_only (bool): response data without head status
                    code and headers. Default is True.
                _preload_content (bool): if False, the urllib3.HTTPResponse object
                    will be returned without reading/decoding response data.
                    Default is True.
                _request_timeout (float/tuple): timeout setting for this request. If one
                    number provided, it will be total request timeout. It can also
                    be a pair (tuple) of (connection, read) timeouts.
                    Default is None.
                _check_input_type (bool): specifies if type checking
                    should be done one the data sent to the server.
                    Default is True.
                _check_return_type (bool): specifies if type checking
                    should be done one the data received from the server.
                    Default is True.
                _host_index (int/None): specifies the index of the server
                    that we want to use.
                    Default is read from the configuration.
                async_req (bool): execute request asynchronously

            Returns:
                WellHeader
                    If the method is called asynchronously, returns the request
                    thread.
            """
            kwargs['async_req'] = kwargs.get(
                'async_req', False
            )
            kwargs['_return_http_data_only'] = kwargs.get(
                '_return_http_data_only', True
            )
            kwargs['_preload_content'] = kwargs.get(
                '_preload_content', True
            )
            kwargs['_request_timeout'] = kwargs.get(
                '_request_timeout', None
            )
            kwargs['_check_input_type'] = kwargs.get(
                '_check_input_type', True
            )
            kwargs['_check_return_type'] = kwargs.get(
                '_check_return_type', True
            )
            kwargs['_host_index'] = kwargs.get('_host_index')
            kwargs['well_header_input'] = \
                well_header_input
            return self.call_with_http_info(**kwargs)

        self.production_add_well_header = Endpoint(
            settings={
                'response_type': (WellHeader,),
                'auth': [
                    'bearerAuth'
                ],
                'endpoint_path': '/api/production/wellheader',
                'operation_id': 'production_add_well_header',
                'http_method': 'PUT',
                'servers': None,
            },
            params_map={
                'all': [
                    'well_header_input',
                ],
                'required': [
                    'well_header_input',
                ],
                'nullable': [
                ],
                'enum': [
                ],
                'validation': [
                ]
            },
            root_map={
                'validations': {
                },
                'allowed_values': {
                },
                'openapi_types': {
                    'well_header_input':
                        (WellHeaderInput,),
                },
                'attribute_map': {
                },
                'location_map': {
                    'well_header_input': 'body',
                },
                'collection_format_map': {
                }
            },
            headers_map={
                'accept': [
                    'application/json'
                ],
                'content_type': [
                    'application/json'
                ]
            },
            api_client=api_client,
            callable=__production_add_well_header
        )

        def __production_add_well_headers(
            self,
            well_header_input,
            **kwargs
        ):
            """production_add_well_headers  # noqa: E501

            This method makes a synchronous HTTP request by default. To make an
            asynchronous HTTP request, please pass async_req=True

            >>> thread = api.production_add_well_headers(well_header_input, async_req=True)
            >>> result = thread.get()

            Args:
                well_header_input ([WellHeaderInput]):

            Keyword Args:
                _return_http_data_only (bool): response data without head status
                    code and headers. Default is True.
                _preload_content (bool): if False, the urllib3.HTTPResponse object
                    will be returned without reading/decoding response data.
                    Default is True.
                _request_timeout (float/tuple): timeout setting for this request. If one
                    number provided, it will be total request timeout. It can also
                    be a pair (tuple) of (connection, read) timeouts.
                    Default is None.
                _check_input_type (bool): specifies if type checking
                    should be done one the data sent to the server.
                    Default is True.
                _check_return_type (bool): specifies if type checking
                    should be done one the data received from the server.
                    Default is True.
                _host_index (int/None): specifies the index of the server
                    that we want to use.
                    Default is read from the configuration.
                async_req (bool): execute request asynchronously

            Returns:
                [WellHeader]
                    If the method is called asynchronously, returns the request
                    thread.
            """
            kwargs['async_req'] = kwargs.get(
                'async_req', False
            )
            kwargs['_return_http_data_only'] = kwargs.get(
                '_return_http_data_only', True
            )
            kwargs['_preload_content'] = kwargs.get(
                '_preload_content', True
            )
            kwargs['_request_timeout'] = kwargs.get(
                '_request_timeout', None
            )
            kwargs['_check_input_type'] = kwargs.get(
                '_check_input_type', True
            )
            kwargs['_check_return_type'] = kwargs.get(
                '_check_return_type', True
            )
            kwargs['_host_index'] = kwargs.get('_host_index')
            kwargs['well_header_input'] = \
                well_header_input
            return self.call_with_http_info(**kwargs)

        self.production_add_well_headers = Endpoint(
            settings={
                'response_type': ([WellHeader],),
                'auth': [
                    'bearerAuth'
                ],
                'endpoint_path': '/api/production/wellheaders',
                'operation_id': 'production_add_well_headers',
                'http_method': 'PUT',
                'servers': None,
            },
            params_map={
                'all': [
                    'well_header_input',
                ],
                'required': [
                    'well_header_input',
                ],
                'nullable': [
                ],
                'enum': [
                ],
                'validation': [
                ]
            },
            root_map={
                'validations': {
                },
                'allowed_values': {
                },
                'openapi_types': {
                    'well_header_input':
                        ([WellHeaderInput],),
                },
                'attribute_map': {
                },
                'location_map': {
                    'well_header_input': 'body',
                },
                'collection_format_map': {
                }
            },
            headers_map={
                'accept': [
                    'application/json'
                ],
                'content_type': [
                    'application/json'
                ]
            },
            api_client=api_client,
            callable=__production_add_well_headers
        )

        def __production_delete_well_header(
            self,
            uwi,
            **kwargs
        ):
            """production_delete_well_header  # noqa: E501

            This method makes a synchronous HTTP request by default. To make an
            asynchronous HTTP request, please pass async_req=True

            >>> thread = api.production_delete_well_header(uwi, async_req=True)
            >>> result = thread.get()

            Args:
                uwi (str):

            Keyword Args:
                _return_http_data_only (bool): response data without head status
                    code and headers. Default is True.
                _preload_content (bool): if False, the urllib3.HTTPResponse object
                    will be returned without reading/decoding response data.
                    Default is True.
                _request_timeout (float/tuple): timeout setting for this request. If one
                    number provided, it will be total request timeout. It can also
                    be a pair (tuple) of (connection, read) timeouts.
                    Default is None.
                _check_input_type (bool): specifies if type checking
                    should be done one the data sent to the server.
                    Default is True.
                _check_return_type (bool): specifies if type checking
                    should be done one the data received from the server.
                    Default is True.
                _host_index (int/None): specifies the index of the server
                    that we want to use.
                    Default is read from the configuration.
                async_req (bool): execute request asynchronously

            Returns:
                bool
                    If the method is called asynchronously, returns the request
                    thread.
            """
            kwargs['async_req'] = kwargs.get(
                'async_req', False
            )
            kwargs['_return_http_data_only'] = kwargs.get(
                '_return_http_data_only', True
            )
            kwargs['_preload_content'] = kwargs.get(
                '_preload_content', True
            )
            kwargs['_request_timeout'] = kwargs.get(
                '_request_timeout', None
            )
            kwargs['_check_input_type'] = kwargs.get(
                '_check_input_type', True
            )
            kwargs['_check_return_type'] = kwargs.get(
                '_check_return_type', True
            )
            kwargs['_host_index'] = kwargs.get('_host_index')
            kwargs['uwi'] = \
                uwi
            return self.call_with_http_info(**kwargs)

        self.production_delete_well_header = Endpoint(
            settings={
                'response_type': (bool,),
                'auth': [
                    'bearerAuth'
                ],
                'endpoint_path': '/api/production/wellheader/{uwi}',
                'operation_id': 'production_delete_well_header',
                'http_method': 'DELETE',
                'servers': None,
            },
            params_map={
                'all': [
                    'uwi',
                ],
                'required': [
                    'uwi',
                ],
                'nullable': [
                ],
                'enum': [
                ],
                'validation': [
                ]
            },
            root_map={
                'validations': {
                },
                'allowed_values': {
                },
                'openapi_types': {
                    'uwi':
                        (str,),
                },
                'attribute_map': {
                    'uwi': 'uwi',
                },
                'location_map': {
                    'uwi': 'path',
                },
                'collection_format_map': {
                }
            },
            headers_map={
                'accept': [
                    'application/json'
                ],
                'content_type': [],
            },
            api_client=api_client,
            callable=__production_delete_well_header
        )

        def __production_get_well_header(
            self,
            uwi,
            **kwargs
        ):
            """production_get_well_header  # noqa: E501

            This method makes a synchronous HTTP request by default. To make an
            asynchronous HTTP request, please pass async_req=True

            >>> thread = api.production_get_well_header(uwi, async_req=True)
            >>> result = thread.get()

            Args:
                uwi (str):

            Keyword Args:
                _return_http_data_only (bool): response data without head status
                    code and headers. Default is True.
                _preload_content (bool): if False, the urllib3.HTTPResponse object
                    will be returned without reading/decoding response data.
                    Default is True.
                _request_timeout (float/tuple): timeout setting for this request. If one
                    number provided, it will be total request timeout. It can also
                    be a pair (tuple) of (connection, read) timeouts.
                    Default is None.
                _check_input_type (bool): specifies if type checking
                    should be done one the data sent to the server.
                    Default is True.
                _check_return_type (bool): specifies if type checking
                    should be done one the data received from the server.
                    Default is True.
                _host_index (int/None): specifies the index of the server
                    that we want to use.
                    Default is read from the configuration.
                async_req (bool): execute request asynchronously

            Returns:
                WellHeader
                    If the method is called asynchronously, returns the request
                    thread.
            """
            kwargs['async_req'] = kwargs.get(
                'async_req', False
            )
            kwargs['_return_http_data_only'] = kwargs.get(
                '_return_http_data_only', True
            )
            kwargs['_preload_content'] = kwargs.get(
                '_preload_content', True
            )
            kwargs['_request_timeout'] = kwargs.get(
                '_request_timeout', None
            )
            kwargs['_check_input_type'] = kwargs.get(
                '_check_input_type', True
            )
            kwargs['_check_return_type'] = kwargs.get(
                '_check_return_type', True
            )
            kwargs['_host_index'] = kwargs.get('_host_index')
            kwargs['uwi'] = \
                uwi
            return self.call_with_http_info(**kwargs)

        self.production_get_well_header = Endpoint(
            settings={
                'response_type': (WellHeader,),
                'auth': [
                    'bearerAuth'
                ],
                'endpoint_path': '/api/production/wellheader/{uwi}',
                'operation_id': 'production_get_well_header',
                'http_method': 'GET',
                'servers': None,
            },
            params_map={
                'all': [
                    'uwi',
                ],
                'required': [
                    'uwi',
                ],
                'nullable': [
                ],
                'enum': [
                ],
                'validation': [
                ]
            },
            root_map={
                'validations': {
                },
                'allowed_values': {
                },
                'openapi_types': {
                    'uwi':
                        (str,),
                },
                'attribute_map': {
                    'uwi': 'uwi',
                },
                'location_map': {
                    'uwi': 'path',
                },
                'collection_format_map': {
                }
            },
            headers_map={
                'accept': [
                    '*/*'
                ],
                'content_type': [],
            },
            api_client=api_client,
            callable=__production_get_well_header
        )

        def __production_get_well_headers(
            self,
            **kwargs
        ):
            """production_get_well_headers  # noqa: E501

            This method makes a synchronous HTTP request by default. To make an
            asynchronous HTTP request, please pass async_req=True

            >>> thread = api.production_get_well_headers(async_req=True)
            >>> result = thread.get()


            Keyword Args:
                offset (int): [optional]
                limit (int): [optional]
                _return_http_data_only (bool): response data without head status
                    code and headers. Default is True.
                _preload_content (bool): if False, the urllib3.HTTPResponse object
                    will be returned without reading/decoding response data.
                    Default is True.
                _request_timeout (float/tuple): timeout setting for this request. If one
                    number provided, it will be total request timeout. It can also
                    be a pair (tuple) of (connection, read) timeouts.
                    Default is None.
                _check_input_type (bool): specifies if type checking
                    should be done one the data sent to the server.
                    Default is True.
                _check_return_type (bool): specifies if type checking
                    should be done one the data received from the server.
                    Default is True.
                _host_index (int/None): specifies the index of the server
                    that we want to use.
                    Default is read from the configuration.
                async_req (bool): execute request asynchronously

            Returns:
                [WellHeader]
                    If the method is called asynchronously, returns the request
                    thread.
            """
            kwargs['async_req'] = kwargs.get(
                'async_req', False
            )
            kwargs['_return_http_data_only'] = kwargs.get(
                '_return_http_data_only', True
            )
            kwargs['_preload_content'] = kwargs.get(
                '_preload_content', True
            )
            kwargs['_request_timeout'] = kwargs.get(
                '_request_timeout', None
            )
            kwargs['_check_input_type'] = kwargs.get(
                '_check_input_type', True
            )
            kwargs['_check_return_type'] = kwargs.get(
                '_check_return_type', True
            )
            kwargs['_host_index'] = kwargs.get('_host_index')
            return self.call_with_http_info(**kwargs)

        self.production_get_well_headers = Endpoint(
            settings={
                'response_type': ([WellHeader],),
                'auth': [
                    'bearerAuth'
                ],
                'endpoint_path': '/api/production/wellheaders',
                'operation_id': 'production_get_well_headers',
                'http_method': 'GET',
                'servers': None,
            },
            params_map={
                'all': [
                    'offset',
                    'limit',
                ],
                'required': [],
                'nullable': [
                ],
                'enum': [
                ],
                'validation': [
                ]
            },
            root_map={
                'validations': {
                },
                'allowed_values': {
                },
                'openapi_types': {
                    'offset':
                        (int,),
                    'limit':
                        (int,),
                },
                'attribute_map': {
                    'offset': 'offset',
                    'limit': 'limit',
                },
                'location_map': {
                    'offset': 'query',
                    'limit': 'query',
                },
                'collection_format_map': {
                }
            },
            headers_map={
                'accept': [
                    'application/json'
                ],
                'content_type': [],
            },
            api_client=api_client,
            callable=__production_get_well_headers
        )

        def __production_update_well_header(
            self,
            well_header,
            **kwargs
        ):
            """production_update_well_header  # noqa: E501

            This method makes a synchronous HTTP request by default. To make an
            asynchronous HTTP request, please pass async_req=True

            >>> thread = api.production_update_well_header(well_header, async_req=True)
            >>> result = thread.get()

            Args:
                well_header (WellHeader):

            Keyword Args:
                _return_http_data_only (bool): response data without head status
                    code and headers. Default is True.
                _preload_content (bool): if False, the urllib3.HTTPResponse object
                    will be returned without reading/decoding response data.
                    Default is True.
                _request_timeout (float/tuple): timeout setting for this request. If one
                    number provided, it will be total request timeout. It can also
                    be a pair (tuple) of (connection, read) timeouts.
                    Default is None.
                _check_input_type (bool): specifies if type checking
                    should be done one the data sent to the server.
                    Default is True.
                _check_return_type (bool): specifies if type checking
                    should be done one the data received from the server.
                    Default is True.
                _host_index (int/None): specifies the index of the server
                    that we want to use.
                    Default is read from the configuration.
                async_req (bool): execute request asynchronously

            Returns:
                WellHeader
                    If the method is called asynchronously, returns the request
                    thread.
            """
            kwargs['async_req'] = kwargs.get(
                'async_req', False
            )
            kwargs['_return_http_data_only'] = kwargs.get(
                '_return_http_data_only', True
            )
            kwargs['_preload_content'] = kwargs.get(
                '_preload_content', True
            )
            kwargs['_request_timeout'] = kwargs.get(
                '_request_timeout', None
            )
            kwargs['_check_input_type'] = kwargs.get(
                '_check_input_type', True
            )
            kwargs['_check_return_type'] = kwargs.get(
                '_check_return_type', True
            )
            kwargs['_host_index'] = kwargs.get('_host_index')
            kwargs['well_header'] = \
                well_header
            return self.call_with_http_info(**kwargs)

        self.production_update_well_header = Endpoint(
            settings={
                'response_type': (WellHeader,),
                'auth': [
                    'bearerAuth'
                ],
                'endpoint_path': '/api/production/wellheader',
                'operation_id': 'production_update_well_header',
                'http_method': 'PATCH',
                'servers': None,
            },
            params_map={
                'all': [
                    'well_header',
                ],
                'required': [
                    'well_header',
                ],
                'nullable': [
                ],
                'enum': [
                ],
                'validation': [
                ]
            },
            root_map={
                'validations': {
                },
                'allowed_values': {
                },
                'openapi_types': {
                    'well_header':
                        (WellHeader,),
                },
                'attribute_map': {
                },
                'location_map': {
                    'well_header': 'body',
                },
                'collection_format_map': {
                }
            },
            headers_map={
                'accept': [
                    '*/*'
                ],
                'content_type': [
                    'application/json'
                ]
            },
            api_client=api_client,
            callable=__production_update_well_header
        )