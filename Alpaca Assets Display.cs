using System;
using System.Linq;
using System.Threading.Tasks;
using Alpaca.Markets;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Alpaca API Console Application - Retrieve and Display Assets");
        await RetrieveAndDisplayAssetsAsync();
    }

    static async Task RetrieveAndDisplayAssetsAsync()
    {
        try
        {
            var alpacaClient = Environments.Paper.GetAlpacaTradingClient(new SecretKey("PKQXY66TMDHLUTYEWVXG", "YNYnaq3s80lgN5MAb2E4QQmyePWr49kFHL6XU6aq"));
            var assets = await alpacaClient.ListAssetsAsync();

            if (assets.Any())
            {
                Console.WriteLine("\nAssets:");
                foreach (var asset in assets)
                {
                    Console.WriteLine($"Asset ID: {asset.AssetId}");
                    Console.WriteLine($"Symbol: {asset.Symbol}");
                    Console.WriteLine($"Exchange: {asset.Exchange}");
                    Console.WriteLine($"Asset Class: {asset.Class}");
                    Console.WriteLine($"Status: {asset.Status}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No assets found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
