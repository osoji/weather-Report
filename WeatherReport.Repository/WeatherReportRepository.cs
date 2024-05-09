using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherReport.Domain;
using WeatherReport.Infrastructure;
using WeatherReport.Repository.Interface;
using WeatherReport.ViewModel;

namespace WeatherReport.Repository
{
    public class WeatherReportRepository : IWeatherReportRepository
    {
        private readonly WeatherDbContext _context;
        public WeatherReportRepository(WeatherDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddWeatherReport(List<WeatherDTO> weather)
        {
            var previousWeatherReport = _context.Weathers.ToList();
            if (previousWeatherReport.Any())
            {
                _context.Weathers.RemoveRange(previousWeatherReport);
            }

            var weatherReportList = weather.Select(x => new Weather()
            {
                AvTempratureC = x.AvTempratureC,
                AvTempratureF = x.AvTempratureF,
                City = x.City,
                Icon = x.Icon,
                Country = x.Country,
                ForecastDate = x.ForecastDate,
                Humidity = x.Humidity,
                MaxWindKPH = x.MaxWindKPH,
                MaxWindMPH = x.MaxWindMPH,
                WeatherCondition = x.WeatherCondition,
                WeatherHistoryId = x.WeatherHistoryId,
            });

            await _context.Weathers.AddRangeAsync(weatherReportList);

            return await SaveAll();
        }

        public async Task<IReadOnlyList<WeatherDTO>> FetchWeatherReport()
        {
            return await _context.Weathers.Select(x => new WeatherDTO()
            {
                AvTempratureC = x.AvTempratureC,
                AvTempratureF = x.AvTempratureF,
                City = x.City,
                Icon = x.Icon,
                Country = x.Country,
                ForecastDate = x.ForecastDate,
                Humidity = x.Humidity,
                MaxWindKPH = x.MaxWindKPH,
                MaxWindMPH = x.MaxWindMPH,
                WeatherCondition = x.WeatherCondition,
                WeatherHistoryId = x.WeatherHistoryId,
            }).ToListAsync();
        }
    }
}
