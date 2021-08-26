using CodeFlowRaw.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace CodeFlowRaw.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            var url = new StringBuilder()
                .Append("https://localhost:5001/connect/authorize?")
                .Append("response_type=code")
                .Append("&scope=openid profile read-diaries")
                .Append("&client_id=mvc-raw")
                .Append("&redirect_uri=https://localhost:5056/Login/Callback")
                .ToString();

            return Redirect(url);
        }

        public async Task<IActionResult> Callback(string code, string state)
        {
            var token = await ExchangeCodeForToken(code);
            var userInfo = await GetUserInfo(token);

            var vm = new LoginCallbackViewModel()
            {
                Token = token,
                UserInfo = userInfo,
            };

            return View(vm);
        }

        private static async Task<TokenResponse> ExchangeCodeForToken(string code)
        {
            var requestBody = new StringBuilder()
                .Append("grant_type=authorization_code")
                .Append($"&code={code}")
                .Append("&redirect_uri=https://localhost:5056/Login/Callback")
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
        private static async Task<UserInfoResponse> GetUserInfo(TokenResponse token)
        {
            var client = new RestClient($"https://localhost:5001/connect/userinfo");
            client.UseNewtonsoftJson();
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"{token.TokenType} {token.AccessToken}");

            var response = await client.ExecuteAsync<UserInfoResponse>(request);
            return response.Data;
        }
        private static string ToBase64(string value)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
