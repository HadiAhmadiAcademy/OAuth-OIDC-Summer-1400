using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Spectre.Console;

namespace ClientCredentialsFlowRaw
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press any key to get token...");
            Console.ReadLine();
            Console.WriteLine("---------------------------");

            var token = await GetToken();
            Console.WriteLine("Access Token : ");
            Console.WriteLine(token.AccessToken);

            Console.WriteLine("---------------------------");
            Console.WriteLine("Press any key call api with token...");
            Console.ReadLine();

            var diaries = await GetDiaries(token);

            ShowDiaries(diaries);

            Console.ReadLine();
        }
        private static async Task<TokenResponse> GetToken()
        {
            var requestBody = new StringBuilder()
                .Append("grant_type=client_credentials")
                //.Append("&client_id=console-client-credentials-raw")
                //.Append("&client_secret=console-client-credentials-raw-secret")
                .Append("&scope=read-diaries")
                .ToString();

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("console-client-credentials-raw:console-client-credentials-raw-secret");
            var clientPasswordBase64 = System.Convert.ToBase64String(plainTextBytes);

            var client = new RestClient($"https://localhost:5001/connect/token");
            client.UseNewtonsoftJson();
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("authorization", $"Basic {clientPasswordBase64}");
            request.AddParameter("application/x-www-form-urlencoded", requestBody, ParameterType.RequestBody);
            var response = await client.ExecuteAsync<TokenResponse>(request);
            return response.Data;
        }
        public static async Task<List<Diary>> GetDiaries(TokenResponse token)
        {
            var client = new RestClient($"https://localhost:5005/api/diaries/all");
            client.UseNewtonsoftJson();
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"{token.TokenType} {token.AccessToken}");
            var diaries = await client.ExecuteAsync<List<Diary>>(request);
            return diaries.Data;
        }
        private static void ShowDiaries(List<Diary> diaries)
        {
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("User Id");
            table.AddColumn("Date");
            table.AddColumn("Text");

            foreach (var entry in diaries)
            {
                var text = entry.Text.Length > 10 ? entry.Text.Substring(0, 10) + "..." : entry.Text;
                table.AddRow(entry.Id.ToString(), entry.UserId.ToString(), entry.DiaryDate.ToShortDateString(), text);
            }
            AnsiConsole.Render(table);
        }
    }
}
