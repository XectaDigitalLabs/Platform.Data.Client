
# Platform Data Client Python

### Generating the xecta openapi client.

We leverage gradle to execute the swagger client code generation. This function will generate all the necessary
client code to communicate with the api.

```shell
../gradlew generateSwaggerCode-python
```

### Xecta Api Initialization

```python
    # Initialize the xecta api with the root url, certificate and key
    xecta_api = XectaApi("https://testawsapi.onxecta.com", '/home/someuser/.auth/my_client.pem', '/home/someuser/.auth/my_client.key')
    # Authenticating will return you a xecta api client.
    api_client = xecta_api.authenticate('<aaaabbbbccccdddeeee>','fffggghhhhiiiiijjjjkkkkllllmmmnnnoooppp')

```


### Well Header
TBD
```python

well_header_input = WellHeaderInput(
    uwi="well_uwi",
    name="well 1",
    group="group 1",
    field="field 1",
    type="INJECTOR",
    fluid="GAS",
    route="route",
    lift_type="GAS_LIFT",
    lat=0.0,
    lon=0.0
)

try:
    well_header_api = authenticated_client.well_header_api()
    api_response = well_header_api.production_add_well_header(well_header_input)
    pprint(api_response)
except openapi_client.ApiException as e:
    print("Exception when calling DailyProductionApi->production_add_daily: %s\n" % e)

```


### Daily Production
Sample code saving daily production

#### Adding dailing production record

```python

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
        api_response = api_client.daily_production_api().production_add_daily(daily_production_input)
        pprint(api_response)
    except openapi_client.ApiException as e:
        print("Exception when calling DailyProductionApi->production_add_daily: %s\n" % e)
```