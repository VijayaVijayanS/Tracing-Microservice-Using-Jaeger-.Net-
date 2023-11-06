using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Alpaca.Markets;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LambdaService
{
    public class Function
    {
        private static TracerProvider tracerProvider;

        static Function()
        {
            
            tracerProvider = Sdk.CreateTracerProviderBuilder()
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("LambdaService"))
                .AddSource("LambdaService")
                .AddJaegerExporter(o =>
                {
                    o.AgentHost = "16.170.253.103"; 
                    o.AgentPort = 6831; 
                })
                .Build();
        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                Console.WriteLine("Alpaca API Lambda Function - Retrieve and Display Assets");

                
                using var span = tracerProvider.GetTracer("LambdaService").StartActiveSpan("LambdaExecution");

                var alpacaClient = Environments.Paper.GetAlpacaTradingClient(new SecretKey("PKF4SCCQZG73FYVYS459", "g7xozYEtaVIp1MhxXcULaDbjK1Y32X4jEJoeROhg"));
                var assets = await alpacaClient.ListAssetsAsync(new AssetsRequest { AssetStatus = AssetStatus.Active });

                List<AssetDetails> assetDetailsList = new List<AssetDetails>();

                if (assets.Any())
                {       
                    foreach (var asset in assets)
                    {
                        string assetStatusString = MapAssetStatusToString(asset.Status.ToString()); 

                        assetDetailsList.Add(new AssetDetails
                        {
                            Symbol = asset.Symbol,
                            Name = asset.Name,
                            Exchange = asset.Exchange.ToString(),
                            Tradable = asset.IsTradable ? "Yes" : "No",
                            Status = assetStatusString
                        });
                    }
                }

                string responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(assetDetailsList);

                
                span.SetAttribute("AssetCount", assets.Count);

                return new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = responseJson,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }
            catch (Exception ex)
            {
                
                using var span = tracerProvider.GetTracer("LambdaService").StartActiveSpan("ErrorHandling");

                
                span.SetAttribute("ErrorMessage", ex.Message);

                return new APIGatewayProxyResponse
                {
                    StatusCode = 500,
                    Body = $"{{ \"error\": \"{ex.Message}\" }}",
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }
        }

        public class AssetDetails
        {
            public string Symbol { get; set; }
            public string Name { get; set; }
            public string Exchange { get; set; }
            public string Tradable { get; set; }
            public string Status { get; set; }
        }

        private string MapAssetStatusToString(string assetStatus)
        {
            
            switch (assetStatus)
            {
                case "active":
                    return "Active";
                case "inactive":
                    return "Inactive";
                default:
                    return "Unknown";
            }
        }
    }
}
