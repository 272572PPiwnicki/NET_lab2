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

## 🌲 Drzewo projektu

![image](https://github.com/user-attachments/assets/33915a19-eb82-4382-a8bc-ffa7b83d0b0e)

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

## 🔍 Kluczowe klasy

- **City** – przechowuje nazwę miasta i powiązane pomiary (`WeatherEntry`).
- **WeatherEntry** – pojedynczy rekord pogodowy (temperatura, wilgotność, ciśnienie, data).
- **WeatherDbContext** – konfiguracja bazy danych EF Core, relacja 1:N, unikalny indeks (CityId + Date).
![image](https://github.com/user-attachments/assets/91b4ffe2-d2d5-4fa6-94d7-2b243ee16222)


- **WeatherData / MainInfo** – klasy służące do deserializacji danych pogodowych z JSON (API).
- **Program** – logika aplikacji konsolowej: menu, pobieranie danych z API, zapis do bazy, wyświetlanie.
- **ShowAllWeather()** - metoda w klasie Program, wyświetlająca zapisane pomiary pogodowe z bazy danych.
![image](https://github.com/user-attachments/assets/a6132d9f-d4cc-4fa4-883f-9397527f3b2e)
![image](https://github.com/user-attachments/assets/43dd8631-742b-4325-9308-70fdd8740dfe)

---
