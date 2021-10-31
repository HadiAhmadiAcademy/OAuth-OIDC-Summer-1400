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
            //var actorToken = await RopcService.GetToken("service-api1", "Pa$$word123");
            var subjectToken = ReadTokenFromHeader();
            //var newToken = await TokenExchangeService.ExchangeForDelegation(subjectToken, actorToken.AccessToken);

            var newToken = await TokenExchangeService.ExchangeForImpersonation(subjectToken);

            return null;
        }

        private string ReadTokenFromHeader()
        {
            var header = Request.Headers["Authorization"];
            return header.ToString().Split(" ")[1];
        }
    }
}
