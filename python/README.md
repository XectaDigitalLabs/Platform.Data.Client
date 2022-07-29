
# Platform Data Client Python


### Xecta Api Initialization


```python

```



### Well Header



### Daily Production
Sample code saving daily production

```python
    
    x = XectaApi("https://testawsapi.onxecta.com", '/home/someuser/.auth/my_client.pem', '/home/someuser/.auth/my_client.key', )

    authenticated_client = x.authenticate('<aaaabbbbccccdddeeee>','fffggghhhhiiiiijjjjkkkkllllmmmnnnoooppp')
    # authenticated_client.add_daily_production()

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
        api_response = authenticated_client.daily_production_api().production_add_daily(daily_production_input)
        pprint(api_response)
    except openapi_client.ApiException as e:
        print("Exception when calling DailyProductionApi->production_add_daily: %s\n" % e)
```