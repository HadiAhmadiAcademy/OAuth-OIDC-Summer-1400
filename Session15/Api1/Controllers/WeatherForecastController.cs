using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api1.Services;
using FizzWare.NBuilder;

namespace Api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var token = ReadTokenFromHeader();
            var newToken = await TokenExchangeService.Exchange(token);

            return null;
        }

        private string ReadTokenFromHeader()
        {
            var header = Request.Headers["Authorization"];
            return header.ToString().Split(" ")[1];
        }
    }
}
