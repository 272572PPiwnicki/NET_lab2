using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppExercise
{
    public class City
    {
        public int Id { get; set; } // PK
        public string Name { get; set; }

        public List<WeatherEntry> WeatherEntries { get; set; } // relacja: miasto moze miec wiele pomiarow
    }
}
