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
        public DbSet<City> Cities { get; set; }
        public DbSet<WeatherEntry> WeatherEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=weather.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherEntry>() // jeden pomiar dziennie na miasto
                .HasIndex(e => new { e.CityId, e.Date })
                .IsUnique();

            modelBuilder.Entity<City>()
                .HasMany(c => c.WeatherEntries)
                .WithOne(e => e.City)
                .HasForeignKey(e => e.CityId);
        }
    }
}
