using DSMS.Application.Models.WeatherForecast;

namespace DSMS.Application.Services;

public interface IWeatherForecastService
{
    public Task<IEnumerable<WeatherForecastResponseModel>> GetAsync();
}
