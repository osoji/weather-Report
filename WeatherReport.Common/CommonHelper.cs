using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherReport.Common
{
    public static class CommonHelper
    {
        public static DateTime ToCustomDate(this string date)
        {
            var spliteDate = date.Split('-');

            return new DateTime(int.Parse(spliteDate[0]), int.Parse(spliteDate[1]), int.Parse(spliteDate[2]));
        }
    }
}
