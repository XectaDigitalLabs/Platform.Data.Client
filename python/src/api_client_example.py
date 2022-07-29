import base64
import json
from pprint import pprint

import dateutil.parser

import openapi_client
from openapi_client import ApiClient, Configuration
from openapi_client.api import daily_production_api
from openapi_client.model.daily_production_input import DailyProductionInput


class XectaApi:
    """
    This is a simple wrapper around the openapi generated client that adds some extra functionality for mtls auth
    as well as generating access tokens.
    """
    _configuration: Configuration = None

    def __init__(self, base_url: str, client_cert_file: str, client_key_file: str):
        """
        This will boot strap the initial configuration before allowing the client to authenticate. Upon successful
        authentication a XectaApiClient instance will be returned
        :param base_url: The base url without any extra path parameters. example "https://testawsapi.onxecta.com"
        :param client_cert_file: The full path to the client certificate file for mtls.
        :param client_key_file: the full path to the client certificate key for mtls.
        """
        self._configuration = Configuration(host=base_url)

        # Must initialize the configuration token to None initially or the client initialization will fail
        # with an error about missing access_token property.
        self._configuration.access_token = None

        # Initialize your mtls certificate and key that were provided by Xecta onboarding.
        # Note this should be the absolute path to your client certificate and key.
        self._configuration.cert_file = client_cert_file
        self._configuration.key_file = client_key_file

    class XectaApiClient:
        """
        This is an implementation of the api client that executes api functions. An instance of
        this class is returned from the xecta api authenticate function however this can be initialized
        manually if you already have a valid bearer token.
        """

        def __init__(self, configuration, bearer_token: str):
            self._configuration = configuration
            self.bearer_token = bearer_token
            self._configuration.access_token = bearer_token

        def add_daily_production(self):
            # Enter a context with an instance of the API client
            with ApiClient(self._configuration) as api_client:

                # Create an instance of the API class
                api_instance = daily_production_api.DailyProductionApi(api_client)
                daily_production_input = [
                    DailyProductionInput(
                        uwi="0x0123456789",
                        datetime=dateutil.parser.parse('1970-01-01T00:00:00.00Z'),
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
                ]  # [DailyProductionInput] |

                try:
                    api_response = api_instance.production_add_daily(daily_production_input)
                    pprint(api_response)
                except openapi_client.ApiException as e:
                    print("Exception when calling DailyProductionApi->production_add_daily: %s\n" % e)

    def authenticate(self, api_key: str, api_secret: str):
        """
        This function will handle the authentication
        :param api_key:
        :param api_secret:
        :return:
        """
        with ApiClient(self._configuration) as api_client:
            encoded = f"{api_key}:{api_secret}".encode("utf-8")
            basic_auth = base64.b64encode(encoded).decode("utf-8")
            query_params = {"grant_type": "client_credentials"}
            headers = {
                "Content-Type": "application/x-www-form-urlencoded",
                "Authorization": f"Basic {basic_auth}"
            }
            client_response = api_client.rest_client.POST(url=f"{self._configuration.host}/authenticate/oauth2/token",
                                                          headers=headers,
                                                          query_params=query_params)
            response_data = json.loads(client_response.data.decode('utf-8'))
            return XectaApi.XectaApiClient(self._configuration, response_data['access_token'])


if __name__ == '__main__':
    x = XectaApi("https://testawsapi.onxecta.com", '/Users/chinshaw/.auth/my_client.pem', '/Users/chinshaw/.auth/my_client.key', )

    authenticated_client = x.authenticate('2nb92hfq5k77hnc1emqsdgd6l3','199mk23r1jpcf3mg5raus21f5rumf50n6cpj9ile85v43ugftr5s')
    authenticated_client.add_daily_production()
