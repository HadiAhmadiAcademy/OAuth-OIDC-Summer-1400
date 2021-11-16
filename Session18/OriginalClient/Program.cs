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
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Press any key to get token...");
                Console.ReadLine();

                var cert = CreateCertificate();

                var token = await GetToken(cert);

                Console.WriteLine(token.AccessToken);
                Console.WriteLine("-----------------------");
                Console.WriteLine("Press any key to call api...");
                Console.ReadLine();

                var response = await GetForecasts(cert, token);

                foreach (var forecast in response.Data)
                {
                    Console.WriteLine($"{forecast.City} - {forecast.Date.ToShortDateString()} - {forecast.Temperature}");
                }
                Console.WriteLine("-------------------------");
            }
        }

        private static async Task<IRestResponse<List<WeatherForecast>>> GetForecasts(X509Certificate2 cert, TokenResponse token)
        {
            var client = new RestClient("https://localhost:5005")
            {
                ClientCertificates = new X509CertificateCollection(new X509Certificate2Collection() {cert})
            };
            var request = new RestRequest("/weatherforecast", Method.GET);
            request.AddHeader("Authorization", $"Bearer {token.AccessToken}");

            return await client.ExecuteAsync<List<WeatherForecast>>(request);
        }

        private static async Task<TokenResponse> GetToken(X509Certificate2 cert)
        {
            var client = new RestClient("https://localhost:5001")
            {
                ClientCertificates = new X509CertificateCollection(new X509Certificate2Collection() { cert  })
            };
            var clientAuthentication = ToBase64("client-1:client-1-secret");

            var requestBody = new StringBuilder()
                    .Append("grant_type=client_credentials")
                    .Append("&scope=forecasts")
                ;

            var request = new RestRequest("/connect/mtls/token", Method.POST);
            request.AddHeader("Authorization", $"Basic {clientAuthentication}");
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
        private static string ToBase64(string value)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private static X509Certificate2 CreateCertificate()
        {
            return CertificateFactory.CreateClientCertificate();
        }

    }
}
