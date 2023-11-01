using System;
using System.Linq;
using System.Threading.Tasks;
using Alpaca.Markets;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Alpaca API Console Application - Retrieve and Display Assets");

        using var openTelemetry = Sdk.CreateTracerProviderBuilder()
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("AlpacaApp"))
            .AddSource("AlpacaApp")
            .AddHttpClientInstrumentation()
            .AddJaegerExporter(o =>
            {
                o.AgentHost = "54.196.55.118";
                o.AgentPort = 6831;
            })
            .Build();

        var tracer = openTelemetry.GetTracer("AlpacaApp");

        await RetrieveAndDisplayAssetsAsync(tracer);
    }

    static async Task RetrieveAndDisplayAssetsAsync(Tracer tracer) 
    {
        try
        {
            var api = Environments.Paper.GetAlpacaTradingClient(new SecretKey("PKAE1F88O4AUKEYAK9VZ", "G5cPJrAkARfeD3eFQ3QoGfyLmzqfQHz9DrxrZfpX"));
            var assets = await api.ListAssetsAsync(new AssetsRequest { AssetStatus = AssetStatus.Active });

            using (var span = tracer.StartActiveSpan("RetrieveAndDisplayAssets"))
            {
                if (assets.Any())
                {
                    Console.WriteLine("List of Assets:");
                    foreach (var asset in assets)
                    {
                        Console.WriteLine($"Symbol: {asset.Symbol}");
                        Console.WriteLine($"Name: {asset.Name}");
                        Console.WriteLine($"Exchange: {asset.Exchange}");
                        Console.WriteLine($"Tradable: {(asset.IsTradable ? "Yes" : "No")}");
                        Console.WriteLine($"Status: {asset.Status}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No assets found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving assets: {ex.Message}");
        }
    }
}
