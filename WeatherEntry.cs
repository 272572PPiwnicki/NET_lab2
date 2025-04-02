using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppExercise
{
    // encja reprezentujaca jeden wpis pomiaru pogody w konkretnym dniu dla konkretnego miasta
    public class WeatherEntry
    {
        public int Id { get; set; } // PK
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public DateTime Date { get; set; }

        // relacja N:1 z City
        public int CityId { get; set; } // FK
        public City City { get; set; }
    }
}
