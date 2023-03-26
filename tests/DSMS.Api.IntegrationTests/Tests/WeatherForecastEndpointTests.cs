using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using DSMS.Api.IntegrationTests.Config;
using DSMS.Api.IntegrationTests.Helpers;
using DSMS.Application.Models.WeatherForecast;
using NUnit.Framework;

namespace DSMS.Api.IntegrationTests.Tests;

[TestFixture]
public class WeatherForecastEndpointTests : BaseOneTimeSetup
{
    [Test]
    public async Task Login_Should_Return_User_Information_And_Token()
    {
        // Arrange

        // Act
        var apiResponse = await Client.GetAsync("/api/WeatherForecast");

        // Assert
        var response = await ResponseHelper.GetApiResultAsync<IEnumerable<WeatherForecastResponseModel>>(apiResponse);
        CheckResponse.Succeeded(response);
        response.Result.Should().HaveCount(5);
    }
}
