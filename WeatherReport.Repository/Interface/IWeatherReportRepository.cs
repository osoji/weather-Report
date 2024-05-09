using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherReport.ViewModel;

namespace WeatherReport.Repository.Interface
{
    public interface IWeatherReportRepository
    {
        Task<bool> AddWeatherReport(List<WeatherDTO> weather);
        Task<IReadOnlyList<WeatherDTO>> FetchWeatherReport();
    }
}
