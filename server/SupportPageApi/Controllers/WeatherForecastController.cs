using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using SupportPageApi.Models;

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

    private readonly IMongoCollection<SupportMessageEntity> _supportMessageEntity;


    public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<WebSupportDatebaseSettings> webSupportDatebaseSettings)
    {
        _logger = logger;

        MongoClient client = new MongoClient(webSupportDatebaseSettings.Value.ConnectionString);
        _supportMessageEntity = client.GetDatabase(webSupportDatebaseSettings.Value.DatabaseName).GetCollection<SupportMessageEntity>(webSupportDatebaseSettings.Value.CollectionName);
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

    [HttpPost("supportMessage")]
    public async Task<IActionResult> PostSupportMessage(SupportMessagePostParam supportMessagePostParam)
    {
        var one = new SupportMessageEntity();
        one.Email = supportMessagePostParam.Email;
        one.Message = supportMessagePostParam.Message;
        _supportMessageEntity.InsertOne(one);
        return Ok();
    }

    public class SupportMessagePostParam
    {
        public string Email { get; set;} = "";
        public string Message { get; set;} = "";
    }

    [BsonIgnoreExtraElements]
    public class SupportMessageEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set;}

        [BsonElement("Email")]
        public string Email { get; set;} = "";

        [BsonElement("Message")]
        public string Message { get; set;} = "";

        public SupportMessageEntity()
        {
            Id = ObjectId.GenerateNewId();
        }
    }
}
