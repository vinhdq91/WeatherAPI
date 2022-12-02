using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public class WeatherModel
    {
        public Current current { get; set; }
        public Location location { get; set; }
    }

    public class Current
    {
        public double temperature { get; set; }
        public int weather_code { get; set; }
        public string[] weather_descriptions { get; set; }
        public string is_day { get; set; }
        public double cloudcover { get; set; }
        public double uv_index { get; set; }
        public double visibility { get; set; }
        public double wind_speed { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
        public string country { get; set; }
        public string region { get; set; }
    }
}
