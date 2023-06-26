from xecta_data_client.xecta_api import XectaApi

# Root URL
root_url = "https://data.onxecta.com"
### Client ID
client_id = '<Your Client Id'
### Client Secret
client_secret = '<Your Client Secret>'


# Path to Signed CSR Certificate .pem file
signed_cert = "<PATH TO PEM>/xecta-data-api.pem"
# Path to Private Key
private_key = "<PATH TO KEY>/xecta-data-api.key"


# Initialize the xecta api with the root url, certificate and key
xecta_api = XectaApi(root_url, signed_cert, private_key)

# Authenticating will return you a xecta api client.
api_client = xecta_api.authenticate(client_id, client_secret)

###############################
## Import required Python libraries
import pandas as pd
import numpy as np
## Import optional Python libraries
from tqdm import tqdm
## Import required API client and API endpoints
from openapi_client import ApiException
from xecta_data_client.xecta_api import XectaApi
from openapi_client.exceptions import ApiException

## Import wellheader from source data
well_header = pd.read_excel(r'/Users/kylevessey/Documents/Xecta/Data Platform/ProductLaunchDemoData.xlsx',sheet_name='well')

## Create a function to convert source data field names in camel case to snake case
def filed_name_conversion(str):
    return ''.join(['_'+i.lower() if i.isupper() else i for i in str]).lstrip('_')

## Convert source data field names in camel case to snake case
for name in well_header.columns:
    well_header.rename(columns={name : filed_name_conversion(name)},inplace=True)

## Convert NaN to Nonetype
well_header.replace({np.nan: None},inplace=True)

## Convert field 'uwi' and 'type' to str type
well_header['uwi'] = well_header['uwi'].apply(str)
well_header['type'] = well_header['type'].apply(str)

## Convert wellheader data to list form
well_header_input = well_header.to_dict(orient = 'records')

try:
    well_header_api = api_client.well_header_api()
    api_response = well_header_api.upsert_wells(well_header_input)
    ## Print API response when well header data was succesfully upserted
    print(api_response)
except ApiException as e:
    ## Print API response when error occured during upserting
    print("Exception when calling WellApi->upsert_wells: %s\n" % e)
