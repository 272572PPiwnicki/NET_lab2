# ☁️ Weather Data App (.NET 8.0 + EF Core)

Projekt konsolowej aplikacji w języku C# umożliwiającej pobieranie danych pogodowych z zewnętrznego API i zapisywanie ich w lokalnej bazie danych SQLite. Program zawiera relacje pomiędzy encjami i chroni przed duplikowaniem danych.

---

## 📌 Informacje

- **Autor:** *Patryk Piwnicki*
- **Prowadzący:** mgr inż. Michał Jaroszczuk
- **Grupa:** [SR][17:05]
- **Data:** 2 kwietnia 2025

---

## 🔧 Technologie

- C#
- .NET 8.0
- Entity Framework Core 8.0.3
- SQLite
- Visual Studio 2022

---

## ⚙️ Opis działania

1. Użytkownik wpisuje nazwę miasta w konsoli.
2. Program pobiera aktualne dane pogodowe z API OpenWeatherMap.
3. Jeśli dane dla tego miasta i dnia nie istnieją w bazie — zostają zapisane.
4. Użytkownik może przeglądać historię pomiarów w konsoli.

---

## 🌍 Wykorzystane API

- **OpenWeatherMap API**  
  Endpoint: `https://api.openweathermap.org/data/2.5/weather?q={city}&appid={api_key}&units=metric`  
  Dane: temperatura, wilgotność, ciśnienie

---

## 🗃️ Struktura bazy danych

Program wykorzystuje ORM Entity Framework Core i tworzy bazę danych `weather.db` zawierającą:

- **City**
  - `Id` (PK)
  - `Name`
  - relacja: 1:N z `WeatherEntry`

- **WeatherEntry**
  - `Id` (PK)
  - `Temperature`
  - `Humidity`
  - `Pressure`
  - `Date`
  - `CityId` (FK)

---

## 🧩 Główne klasy

- `Program.cs` – logika menu, pobierania danych, zapisu i wyświetlania
- `WeatherDbContext.cs` – konfiguracja bazy i relacji EF Core
- `WeatherEntry.cs` – pojedynczy pomiar pogodowy
- `City.cs` – reprezentacja miasta
- `WeatherData.cs` i `MainInfo.cs` – klasy do deserializacji danych JSON z API

---
