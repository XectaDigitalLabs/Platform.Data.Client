### Platform Data CSharp

This provides an example of how to invoke api requests to the xecta data platform.

```csharp
    class Program
    {
        public static void Main()
        {
            var xapi = new XectaApi("https://testawsapi.onxecta.com", "/home/someuser/.auth/my_client.pem","/home/someuser/.auth/my_client.key");

            var apiClient = xapi.Authenticate("aaaabbbbccccdddeeee", "fffggghhhhiiiiijjjjkkkkllllmmmnnnoooppp");

            var wellHeaderApi = apiClient.WellHeaderApi();
            var wellHeaderInput = new WellHeaderInput(
                uwi: "uwi 1",
                name: "well 1",
                group: "group 1",
                field: "field 1",
                type: WellHeaderInput.TypeEnum.INJECTOR,
                fluid: WellHeaderInput.FluidEnum.GAS,
                route: "route",
                liftType: WellHeaderInput.LiftTypeEnum.GASLIFT,
                lat: 0.0,
                lon: 0.0);

            try
            {
                var wellHeader = wellHeaderApi.ProductionAddWellHeader(wellHeaderInput);
                Console.WriteLine($"Well header saved {wellHeader}");
            }
            catch (ApiException e)
            {
                Console.WriteLine("Exception when calling DailyProductionApi.ProductionAddDaily: " + e.Message);
                Console.WriteLine("Status Code: " + e.ErrorCode);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
```