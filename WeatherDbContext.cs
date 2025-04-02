using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherAppExercise
{
    public class WeatherDbContext : DbContext
    {
        // definicje tabel
        public DbSet<City> Cities { get; set; } // tabela miast
        public DbSet<WeatherEntry> WeatherEntries { get; set; } // tabela pomiarow

        // metoda uruchamiana automatycznie przy uzyciu new WeatherDbContext()
        protected override void OnConfiguring(DbContextOptionsBuilder options) // konfiguracja bazy
        {
            options.UseSqlite("Data Source=weather.db"); // okresla z jaka baza danych ma sie laczyc
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // tworzy unikalny indeks na kombinacji CityId i Date - dla jednego miasto moze byc tylko jeden wpis z dana data
            modelBuilder.Entity<WeatherEntry>()
                .HasIndex(e => new { e.CityId, e.Date })
                .IsUnique();

            // konfiguracja relacji 1:N City-WeatherEntry
            modelBuilder.Entity<City>()
                .HasMany(c => c.WeatherEntries)
                .WithOne(e => e.City)
                .HasForeignKey(e => e.CityId);
        }
    }
}
