using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using Xunit;

public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ProgramTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast");

        // Assert status code
        Assert.Equal(HttpStatusCode.OK, response.StatusCode); // ✅ Corregido: estaba comentado y los argumentos invertidos

        var responseString = await response.Content.ReadAsStringAsync();

        // ✅ Corregido: opciones para ignorar case sensitivity en las propiedades JSON
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(responseString, options);

        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts!.Length);

        foreach (var forecast in forecasts)
        {
            Assert.InRange(forecast.TemperatureC, -20, 55);
            Assert.False(string.IsNullOrEmpty(forecast.Summary));
        }
    }

    private record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556); // ✅ Sin cambios, es correcto
    }
}