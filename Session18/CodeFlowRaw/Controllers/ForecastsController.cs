using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CodeFlowRaw.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace CodeFlowRaw.Controllers
{
    public class ForecastsController : Controller
    {

        private const string CertPath = @"E:\Drive D\Teaching\Hadi Ahmadi Academy\OAuth - OIDC\Samples\Cert\cert.pfx";
        private const string CertPass = "112233";

        public IActionResult Index()
        {
            var url = new StringBuilder()
                .Append("https://localhost:5001/connect/authorize?")
                .Append("response_type=code")
                .Append("&client_id=client-mvc")
                .Append("&redirect_uri=https://localhost:5056/Forecasts/Read")
                .Append("&scope=forecasts")
                .ToString();

            return Redirect(url);
        }

        public async Task<IActionResult> Read(string code, string state)
        {
            var token = await ExchangeCodeForToken(code);
            var forecasts = await GetForecasts(token);

            //token = await RefreshAccessToken(token.RefreshToken); //Just for testing refresh token
            //diaries = await GetDiariesWithToken(token);

            return View(forecasts);
        }

      
        private async Task<List<WeatherForecast>> GetForecasts(TokenResponse token)
        {
            var cert = new X509Certificate2(CertPath, CertPass);
            var client = new RestClient($"https://localhost:5005/WeatherForecast")
            {
                ClientCertificates = new X509CertificateCollection(new X509Certificate2Collection() {cert})
            };
            client.UseNewtonsoftJson();
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"{token.TokenType} {token.AccessToken}");
            var diaries = await client.ExecuteAsync<List<WeatherForecast>>(request);
            return diaries.Data;
        }

        private static async Task<TokenResponse> ExchangeCodeForToken(string code)
        {
            var cert = new X509Certificate2(CertPath, CertPass);

            var requestBody = new StringBuilder()
                .Append("client_id=client-mvc")
                .Append("&grant_type=authorization_code")
                .Append($"&code={code}")
                .Append("&redirect_uri=https://localhost:5056/Forecasts/Read")
                .ToString();


            var client = new RestClient($"https://localhost:5001/connect/mtls/token")
            {
                ClientCertificates = new X509CertificateCollection(new X509Certificate2Collection() { cert })
            };
            client.UseNewtonsoftJson();
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", requestBody, ParameterType.RequestBody);
            var response = await client.ExecuteAsync<TokenResponse>(request);
            return response.Data;
        }

        private async Task<TokenResponse> RefreshAccessToken(string refreshToken)
        {
            var requestBody = new StringBuilder()
                .Append("grant_type=refresh_token")
                .Append($"&refresh_token={refreshToken}")
                .ToString();

            var clientAuthentication = ToBase64("mvc-raw:mvc-raw-secret");

            var client = new RestClient($"https://localhost:5001/connect/token");
            client.UseNewtonsoftJson();
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", $"Basic {clientAuthentication}");
            request.AddParameter("application/x-www-form-urlencoded", requestBody, ParameterType.RequestBody);
            var response = await client.ExecuteAsync<TokenResponse>(request);
            return response.Data;
        }
        private static string ToBase64(string value)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
