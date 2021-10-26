using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;

namespace Api2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Builder<WeatherForecast>.CreateListOfSize(3)
                .All()
                .With(a => a.City = Faker.Address.City())
                .With(a => a.Temperature = Faker.RandomNumber.Next(-10, 40))
                .With(a => a.Date = DateTime.Now.AddDays(1))
                .Build();
        }
    }
}
