using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherReport.Domain
{
    public class Weather
    {
        public int WeatherHistoryId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Icon { get; set; }
        public decimal AvTempratureC { get; set; }
        public decimal AvTempratureF { get; set; }
        public decimal MaxWindMPH { get; set; }
        public decimal MaxWindKPH { get; set; }
        public decimal Humidity { get; set; }
        public string WeatherCondition { get; set; }
        public DateTime ForecastDate { get; set; }
    }
}
