using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OriginalClient.Model;
using OriginalClient.Services;
using Spectre.Console;

namespace OriginalClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press any key to get token...");
            Console.ReadLine();
            var username = AnsiConsole.Ask<string>("What's your Username?");
            var password = AnsiConsole.Ask<string>("What's your password?");
            Console.WriteLine("---------------------------");

            var token = await RopcService.GetToken(username, password);
            Console.WriteLine("Access Token : ");
            Console.WriteLine(token.AccessToken);
            Console.WriteLine("---------------------------");
            Console.WriteLine("Press any key to call the Api-1 :");
            Console.ReadLine();

            var forecasts = await WeatherService.GetWeatherForecasts(token);

            DisplayForecasts(forecasts);

            Console.ReadLine();
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
