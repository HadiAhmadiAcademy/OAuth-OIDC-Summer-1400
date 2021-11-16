using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OriginalClient.Model;
using OriginalClient.Services;
using RestSharp;
using Spectre.Console;

namespace OriginalClient
{
    class Program
    {
        private const string CertPath = @"E:\Drive D\Teaching\Hadi Ahmadi Academy\OAuth - OIDC\Samples\Cert\certificate.pfx";
        private const string CertPass = "112233";
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press any key to get token...");
            Console.ReadLine();

            var token = await GetToken();
            Console.WriteLine(token.AccessToken);
            Console.WriteLine("-----------------------");
            Console.WriteLine("Press any key to call api...");
            Console.ReadLine();

            var cert = new X509Certificate2(CertPath, CertPass);
            var client = new RestClient("https://localhost:5005")
            {
                ClientCertificates = new X509CertificateCollection(new X509Certificate2Collection() { cert })
            };
            var request = new RestRequest("/weatherforecast", Method.GET);
            request.AddHeader("Authorization", $"Bearer {token.AccessToken}");

            var response = await client.ExecuteAsync<List<WeatherForecast>>(request);

            Console.ReadLine();
        }

        private static async Task<TokenResponse> GetToken()
        {
            var cert = new X509Certificate2(CertPath, CertPass);
            var client = new RestClient("https://localhost:5001")
            {
                ClientCertificates = new X509CertificateCollection(new X509Certificate2Collection() {cert})
            };

            var requestBody = new StringBuilder()
                    .Append("client_id=client-1")
                    .Append("&grant_type=client_credentials")
                    .Append("&scope=forecasts")
                ;

            var request = new RestRequest("/connect/mtls/token", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", requestBody, ParameterType.RequestBody);

            var data = await client.ExecuteAsync<TokenResponse>(request);
            return data.Data;
        }

        private static void DisplayForecasts(List<WeatherForecast> forecasts)
        {
            var table = new Table();
            table.AddColumn("City");
            table.AddColumn("Temperature");
            table.AddColumn("Date");

            foreach (var forecast in forecasts)
            {
                table.AddRow(forecast.City, forecast.Temperature.ToString(), forecast.Date.ToShortDateString());
            }
            AnsiConsole.Write(table);
        }
    }
}
