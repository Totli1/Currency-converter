# Конвертер валют (Currency Converter)  

Простое консольное приложение для конвертации валют с использованием актуальных курсов из открытого API.  

## 📌 Возможности  
- Конвертация между популярными валютами (USD, EUR, RUB, KZT и др.)  
- Получение актуальных курсов с [ExchangeRate-API](https://www.exchangerate-api.com/) (или любого другого)  

## 🛠 Технологии  
- **C#** (.NET 6/7/8)  
- **HTTP-запросы** (через `HttpClient`)  
- **JSON** (парсинг ответа API)  

## Код главного файла

```csharp
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


         Console.WriteLine("Введите исходную валюту (USD)");
         string first = Console.ReadLine();
         Console.WriteLine("Введите целевую валюту (RUB)");
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
```
##Код appsettings.json в котором хранится API 
```json
{
  "ApiKey": "https://v6.exchangerate-api.com/v6/0188f496c2b1a16ea17833c5/latest/"
}
```
