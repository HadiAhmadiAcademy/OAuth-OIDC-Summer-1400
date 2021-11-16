using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Api1.Services;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;
using static System.String;

namespace Api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await CheckClientCertificate();

            return Builder<WeatherForecast>.CreateListOfSize(10).Build().ToList();
        }

        private async Task CheckClientCertificate()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var cnfJson = HttpContext.User.FindFirst("cnf")?.Value;
                if (!IsNullOrWhiteSpace(cnfJson))
                {
                    var certResult = await HttpContext.AuthenticateAsync("Certificate");
                    if (!certResult.Succeeded)
                    {
                        await HttpContext.ChallengeAsync("Certificate");
                        return;
                    }

                    var certificate = await HttpContext.Connection.GetClientCertificateAsync();
                    var thumbprint = Base64UrlTextEncoder.Encode(certificate.GetCertHash(HashAlgorithmName.SHA256));
                    var cnf = JObject.Parse(cnfJson);
                    var sha256 = cnf.Value<string>("x5t#S256");
                    if (string.IsNullOrWhiteSpace(sha256) ||  !thumbprint.Equals(sha256, StringComparison.Ordinal))
                    {
                        await HttpContext.ChallengeAsync("token");
                        return;
                    }
                }
            }
        }
    }
}
