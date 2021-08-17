using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Wso2Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Console.WriteLine("Press any key to get token...");
            Console.ReadLine();
            Console.WriteLine("---------------------------");

            var token = await GetToken();
            Console.WriteLine("Access Token : ");
            Console.WriteLine(token.AccessToken);

            Console.ReadLine();
        }
        private static async Task<TokenResponse> GetToken()
        {
            var requestBody = new StringBuilder()
                .Append("grant_type=client_credentials")
                .ToString();

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("nJM66CNe6NEfg_vEPSgQC6ONjYsa:7P9QC8rtdOB0dLJ7YkfScStYjzUa");
            var clientPasswordBase64 = System.Convert.ToBase64String(plainTextBytes);

            var client = new RestClient($"https://192.168.39.31:9443/oauth2/token");
            client.UseNewtonsoftJson();
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("authorization", $"Basic {clientPasswordBase64}");
            request.AddParameter("application/x-www-form-urlencoded", requestBody, ParameterType.RequestBody);
            var response = await client.ExecuteAsync<TokenResponse>(request);
            return response.Data;
        }
    }
}
