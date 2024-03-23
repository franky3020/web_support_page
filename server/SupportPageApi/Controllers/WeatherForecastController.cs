using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace SupportPageApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("db")]
    public string GetDb()
    {
        MongoClient client = new MongoClient("mongodb://root:example@127.0.0.1:27017");
        var test = client.GetDatabase("franky").GetCollection<SampleClassEntity>("test");
        var one = new SampleClassEntity();
        one.message = "franky-2";
        test.InsertOne(one);
        //var sampleClassEntityList = test.Find(_ => true).ToList();
        return "good";
    }

    [BsonIgnoreExtraElements]
    public class SampleClassEntity
    {
        [BsonId]
        public string Id { get; set;}
        public string message { get; set;}
    }


}
