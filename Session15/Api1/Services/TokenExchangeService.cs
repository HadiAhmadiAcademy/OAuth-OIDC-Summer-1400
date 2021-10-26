using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Api1.Model;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Api1.Services
{
    public static class TokenExchangeService
    {
        public static async Task<TokenResponse> Exchange(string token)
        {
            var requestBody = new StringBuilder()
                .Append("grant_type=urn%3Aietf%3Aparams%3Aoauth%3Agrant-type%3Atoken-exchange")
                .Append($"&subject_token={token}")
                .Append($"&scope=locations")
                .Append($"&subject_token_type=urn%3Aietf%3Aparams%3Aoauth%3Atoken-type%3Aaccess_token")
                .ToString();

            var client = new RestClient($"https://localhost:5001/connect/token");
            client.UseNewtonsoftJson();
            var clientAuthentication = ToBase64("api1:api1-secret");

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