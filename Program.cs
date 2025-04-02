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
            while (true)
            {
                Console.WriteLine("\n=== Weather App ===");
                Console.WriteLine("1. Fetch weather data");
                Console.WriteLine("2. Show all saved data");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await FetchWeatherAndSave(); // pobiera dane z API i zapisuje do bazy
                        break;
                    case "2":
                        ShowAllWeather(); // pokazuje dane z bazy
                        break;
                    case "3":
                        Console.WriteLine("Exiting program..."); // wychodzi z programu
                        return;
                    default:
                        Console.WriteLine("Invalid option"); // obsluga nieprawidlowego wyboru
                        break;
                }
            }
        }

        public static async Task FetchWeatherAndSave()
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

            using var db = new WeatherDbContext(); // tworzenie instanci bazy danych
            db.Database.EnsureCreated(); // sprawdzamy czy baza istnieje, jesli nie tworzymy ja automatycznie

            var city = db.Cities.FirstOrDefault(c => c.Name == city_name); // sprawdzamy czy miasto istnieje w bazie

            if (city == null)
            {
                city = new City { Name = city_name };
                db.Cities.Add(city); // dodanie nowego miasta
                db.SaveChanges(); // zapisujemy
            }

            // sprawdzamy czy mamy pomiar z dzisiaj dla danego miasta
            var today = DateTime.Today;
            bool exists = db.WeatherEntries.Any(e => e.CityId == city.Id && e.Date == today);

            if (exists)
            {
                Console.WriteLine($"\nWeather data for {city_name} on {today:yyyy-MM-dd} already exists in the database.");
                return;
            }

            var userURL = $"https://api.openweathermap.org/data/2.5/weather?q={city_name}&appid={api_key}&units=metric"; // pobranie danych pogodowych z API

            try
            {
                var response = await client.GetAsync(userURL); // wysylamy zapytanie HTTP GET

                // jesli odpowiedz nie jest OK - wyswietlamy blad i konczymy
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return;
                }

                var jsonString = await response.Content.ReadAsStringAsync(); // pobieramy tresc odpowiedzi jako string
                var weatherData = JsonSerializer.Deserialize<WeatherData>(jsonString); // deserializujemy JSON do obiektu WeatherData

                if (weatherData != null && weatherData.main != null)
                {
                    var entry = new WeatherEntry // tworzymy nowy wpis na podstawie danych pogodowych
                    {
                        CityId = city.Id,
                        Temperature = weatherData.main.temp,
                        Humidity = weatherData.main.humidity,
                        Pressure = weatherData.main.pressure,
                        Date = today
                    };

                    db.WeatherEntries.Add(entry); // dodajemy
                    db.SaveChanges(); // zapisujemy

                    Console.WriteLine($"\nWeather in {city_name} on {today:yyyy-MM-dd}:");
                    Console.WriteLine($"Temperature: {entry.Temperature}°C");
                    Console.WriteLine($"Humidity: {entry.Humidity}%");
                    Console.WriteLine($"Pressure: {entry.Pressure} hPa");
                    Console.WriteLine("Data saved to database.");
                }
                else
                {
                    Console.WriteLine("Unable to parse weather data.");
                }
            }
            catch (HttpRequestException e) // obsluga bledu HTTP
            {
                Console.WriteLine($"\nHTTP error: {e.Message}");
            }
            catch (Exception e) // obsluga innych bledow
            {
                Console.WriteLine($"\nUnexpected error: {e.Message}");
            }
        }
        public static void ShowAllWeather()
        {
            using var db = new WeatherDbContext();
            var entries = db.WeatherEntries
                            .OrderByDescending(e => e.Date) // sortowanie malejaco po dacie
                            .ThenBy(e => e.City.Name) // gdy data jest ta sama, sortujemy alfabetycznie
                            .ToList();

            if (entries.Count == 0)
            {
                Console.WriteLine("\nNo data in the database");
                return;
            }

            Console.WriteLine("\nSaved weather data:");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("City\t\tDate\t\tTemp [°C]\tHumidity [%]\tPressure [hPa]");
            Console.WriteLine("-------------------------------------------------------------------------------");

            foreach (var entry in entries)
            {
                string city = db.Cities.Find(entry.CityId)?.Name ?? "(unknown)"; // szuka miasta w bazie danych po ID - jesli znajdzie to przypisuje jego nazwe, jesli nie to przypisuje unknown
                Console.WriteLine($"{city,-12}\t{entry.Date:yyyy-MM-dd}\t{entry.Temperature,9:0.00}\t{entry.Humidity,11}%\t{entry.Pressure,14}");
            }
        }
    }
}
