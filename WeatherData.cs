using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppExercise
{
    public class WeatherData // deserializacja glownego obiektu JSON z API
    {
        public MainInfo main { get; set; }
        public string name { get; set; }
    }
}
