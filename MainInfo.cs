using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppExercise
{
    public class MainInfo // dane pogodowe w sekcji 'main' odpowiedzi JSON
    {
        public double temp { get; set; }
        public int humidity { get; set; }
        public int pressure { get; set; }
    }
}
