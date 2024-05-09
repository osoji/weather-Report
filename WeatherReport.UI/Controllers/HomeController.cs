using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using WeatherReport.UI.Models;
using WeatherReport.ViewModel;
using WeatherReport.Common;
using WeatherReport.Repository.Interface;

namespace WeatherReport.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWeatherReportRepository _repository;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IWeatherReportRepository repository)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> DownloadWeatherReport(ModelParameter model)
        {
            try
            {
                var httpUrl = $"http://api.weatherapi.com/v1/forecast.json?key=51f0bd1f214349d28a6151659242904&q={model.city}&days={model.days}&aqi=no&alerts=no";

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, httpUrl);

                var httpClient = _httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                    var weatherResults = await JsonSerializer.DeserializeAsync<ForecastViewModel>(contentStream);

                    if (weatherResults != null)
                    {
                        var weatherDTO = new List<WeatherDTO>();

                        foreach (var weather in weatherResults.forecast.forecastday)
                        {
                            weatherDTO.Add(new WeatherDTO
                            {
                                AvTempratureC = Convert.ToDecimal(weather.day.avgtemp_c),
                                AvTempratureF = Convert.ToDecimal(weather.day.avgtemp_f),
                                City = weatherResults.location.name,
                                Icon = weather.day.condition.icon,
                                Country = weatherResults.location.country,
                                ForecastDate = weather.date.ToCustomDate(),
                                Humidity = weather.day.avghumidity,
                                MaxWindKPH = Convert.ToDecimal(weather.day.maxwind_kph),
                                MaxWindMPH = Convert.ToDecimal(weather.day.maxwind_mph),
                                WeatherCondition = weather.day.condition.text,
                            });
                        }

                        await _repository.AddWeatherReport(weatherDTO);

                        return Json(new { error = false, message = "Saved" });
                    }
                }

                return Json(new { error = true, message = "An error occured" });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "");

                return Json(new { error = true, message = e.Message });
            }
        }

        public async Task<IActionResult> RenderChat()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChatData()
        {
            var weatherReportList = await _repository.FetchWeatherReport();

            var chatData = new List<ChatDataModel>();

            foreach (var forecast in weatherReportList)
            {
                chatData.Add(new ChatDataModel
                {
                    date = forecast.ForecastDate.ToString("dd-MMM-yyyy"),
                    value = forecast.AvTempratureF
                });
            }

            return Json(new { data = chatData });
        }
    }
}
