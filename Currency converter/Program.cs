using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Currency_converter
{
    internal class Program
    {
        static async  Task Main(string[] args)
        {
            using var client = new HttpClient();
            string message = null;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            };


            Console.WriteLine("Введите первую валюту");
            string first = Console.ReadLine();
            Console.WriteLine("Введите вторую валюту");
            string second = Console.ReadLine();

          

            string json = File.ReadAllText("appsettings.json");
            var get = JsonSerializer.Deserialize<GetAPI>(json, options);
            message = await client.GetStringAsync($"{get.ApiKey}{first}");
            

            var rates = JsonSerializer.Deserialize<ExchangeRateResponse>(message, options);
            if (rates.Conversion_rates.TryGetValue(second, out decimal usdRate))
            {
                Console.WriteLine($"1 {first} = {usdRate} {second}");
            }

        }
       
    }

    public class ExchangeRateResponse
    {
        public Dictionary<string, decimal> Conversion_rates { get; set; }
    }
    public class GetAPI
    {
        public string ApiKey { get; set; }
    }

 
}
