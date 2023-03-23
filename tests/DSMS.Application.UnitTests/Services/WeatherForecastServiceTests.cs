using System.Threading.Tasks;
using FluentAssertions;
using DSMS.Application.Services.Impl;
using Xunit;

namespace DSMS.Application.UnitTests.Services;

public class WeatherForecastServiceTests
{
    private readonly WeatherForecastService _sut;

    public WeatherForecastServiceTests()
    {
        _sut = new WeatherForecastService();
    }

    [Fact]
    public async Task GetAsync_Should_Return_List_With_Only_Five_ElementsAsync()
    {
        var result = await _sut.GetAsync();

        result.Should().HaveCount(5);
    }
}
