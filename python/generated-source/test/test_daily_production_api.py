"""
    Production API

    API exposing endpoints for managing well headers and daily production.  # noqa: E501

    The version of the OpenAPI document: 1.0
    Generated by: https://openapi-generator.tech
"""


import unittest

import openapi_client
from openapi_client.api.daily_production_api import DailyProductionApi  # noqa: E501


class TestDailyProductionApi(unittest.TestCase):
    """DailyProductionApi unit test stubs"""

    def setUp(self):
        self.api = DailyProductionApi()  # noqa: E501

    def tearDown(self):
        pass

    def test_production_add_daily(self):
        """Test case for production_add_daily

        """
        pass

    def test_production_delete_daily(self):
        """Test case for production_delete_daily

        """
        pass

    def test_production_get_daily(self):
        """Test case for production_get_daily

        """
        pass


if __name__ == '__main__':
    unittest.main()