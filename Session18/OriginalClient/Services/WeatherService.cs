using System.Collections.Generic;
using System.Threading.Tasks;
using OriginalClient.Model;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace OriginalClient.Services
{
    public static class WeatherService
    {
        public static async Task<List<WeatherForecast>> GetWeatherForecasts(TokenResponse token)
        {
            var client = new RestClient($"https://localhost:5005/WeatherForecast");
            client.UseNewtonsoftJson();
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"{token.TokenType} {token.AccessToken}");
            var response = await client.ExecuteAsync<List<WeatherForecast>>(request);
            return response.Data;
        }
        
    }
}