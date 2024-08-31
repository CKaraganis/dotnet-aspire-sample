namespace AspireSample.ApiService.Controllers;

using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly SqlConnection _sqlConnection;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, SqlConnection sqlConnection)
    {
        _logger = logger;
        _sqlConnection = sqlConnection;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var command = _sqlConnection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT * FROM [dbo].[WeatherForecast]";
        _sqlConnection.Open();
        var results = command.ExecuteReader();
        _sqlConnection.Close();
        results.
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}