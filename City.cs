using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppExercise
{
    // encja reprezentujaca miasto w bazie danych
    public class City
    {
        public int Id { get; set; } // PK
        public string Name { get; set; }

        // relacja 1:N z WeatherEntry
        public List<WeatherEntry> WeatherEntries { get; set; } // lista pomiarow przypisanych do miasta
    }
}
