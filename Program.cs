using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherAppExercise
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // API Key: fc720016e7fe5d2e612a05902983d3ed
            var client = new HttpClient(); // tworzenie klienta HTTP
            var api_key = "fc720016e7fe5d2e612a05902983d3ed";

            Console.Write("\nEnter the city name: ");
            var city_name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(city_name)) // walidacja wejscia
            {
                Console.WriteLine("City name cannot be empty.");
                return;
            }

            var userURL = $"https://api.openweathermap.org/data/2.5/weather?q={city_name}&appid={api_key}&units=metric"; // budowanie URL-a do API

            try
            {
                var response = await client.GetAsync(userURL); // wyslanie zapytania GET do API

                if (!response.IsSuccessStatusCode) // obsluga bledu HTTP
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return;
                }

                var jsonString = await response.Content.ReadAsStringAsync(); // pobranie odpowiedzi jako string
                var weatherData = JsonSerializer.Deserialize<WeatherData>(jsonString); // deserializacja JSON do obiektu WeatherData

                if (weatherData != null && weatherData.main != null) // walidacja deserializacji
                {
                    Console.WriteLine($"\nWeather in {weatherData.name}:");
                    Console.WriteLine($"Temperature: {weatherData.main.temp}°C");
                    Console.WriteLine($"Humidity: {weatherData.main.humidity}%");
                    Console.WriteLine($"Pressure: {weatherData.main.pressure} hPa");
                }
                else
                {
                    Console.WriteLine("Unable to parse weather data");
                }
            }
            catch (HttpRequestException e) // obsluga wyjatku HTTP
            {
                Console.WriteLine($"\nHTTP error: {e.Message}");
            }
            catch (Exception e) // obsluga innych wyjatkow
            {
                Console.WriteLine($"\nUnexpected error: {e.Message}");
            }
        }
    }
}
