using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Spectre.Console;

namespace RopcFlowRaw
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

            var token = await GetToken(username, password);
            Console.WriteLine("Access Token : ");
            Console.WriteLine(token.AccessToken);

            Console.WriteLine("---------------------------");
            var shouldRevoke = AnsiConsole.Confirm("Do you want to revoke the token?");
            if (shouldRevoke)
            {
                var response = TokenRevocationService.Revoke(token.AccessToken);
                Console.WriteLine($"Response Code : {response}");
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Press any key call api with token...");
            Console.ReadLine();

            var diaries = await GetDiaries(token);

            ShowDiaries(diaries);

            Console.ReadLine();
        }
        private static async Task<TokenResponse> GetToken(string username, string password)
        {
            var requestBody = new StringBuilder()
                .Append("grant_type=password")
                .Append($"&username={username}")
                .Append($"&password={password}")
                .Append("&scope=read-diaries")
                .ToString();

            var client = new RestClient($"https://localhost:5001/connect/token");
            client.UseNewtonsoftJson();
            var request = new RestRequest(Method.POST);
            var authorization = EncodingUtil.ToBase64("console-ropc-raw:console-ropc-raw-secret");
            request.AddHeader("Authorization", $"Basic {authorization}");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", requestBody, ParameterType.RequestBody);
            var response = await client.ExecuteAsync<TokenResponse>(request);
            return response.Data;
        }
        public static async Task<List<Diary>> GetDiaries(TokenResponse token)
        {
            var client = new RestClient($"https://localhost:5005/api/diaries");
            client.UseNewtonsoftJson();
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"{token.TokenType} {token.AccessToken}");
            var diaries = await client.ExecuteAsync<List<Diary>>(request);
            return diaries.Data;
        }
        private static void ShowDiaries(List<Diary> diaries)
        {
            if (diaries == null) return;

            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Date");
            table.AddColumn("Text");

            foreach (var entry in diaries)
            {
                var text = entry.Text.Length > 10 ? entry.Text.Substring(0, 10) + "..." : entry.Text;
                table.AddRow(entry.Id.ToString(), entry.DiaryDate.ToShortDateString(), text);
            }
            AnsiConsole.Render(table);
        }
    }
}
