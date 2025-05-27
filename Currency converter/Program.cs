using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Currency_converter
{
    internal class Program
    {
        static async  Task Main(string[] args)
        {
            Console.WriteLine("Введите первую валюту");
            string first = Console.ReadLine();
            Console.WriteLine("Введите вторую валюту");
            string second = Console.ReadLine();
            using var client = new HttpClient();
            string message = await client.GetStringAsync($"https://v6.exchangerate-api.com/v6/0188f496c2b1a16ea17833c5/latest/{first}");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            };
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
 
}
