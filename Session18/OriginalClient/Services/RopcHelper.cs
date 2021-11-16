using System.Text;
using System.Threading.Tasks;
using OriginalClient.Model;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace OriginalClient.Services
{
    public class RopcService
    {
        public static async Task<TokenResponse> GetToken(string username, string password)
        {
            var requestBody = new StringBuilder()
                .Append("grant_type=password")
                .Append($"&username={username}")
                .Append($"&password={password}")
                .Append("&scope=forecasts")
                .ToString();
            var clientAuthentication = ToBase64("original-client:original-client-secret");

            var client = new RestClient($"https://localhost:5001/connect/token");
            client.UseNewtonsoftJson();
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Basic {clientAuthentication}");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
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