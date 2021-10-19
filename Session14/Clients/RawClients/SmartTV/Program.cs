using System;
using System.Threading;
using Spectre.Console;

namespace SmartTV
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to initiate the flow...");
            Console.ReadLine();

            var response = DeviceFlowHelper.SendDeviceAuthorizationRequest("smart-tv");

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Using a browser on another device, visit:");
            Console.WriteLine(response.VerificationUri);
            Console.WriteLine("And enter the code:");
            Console.WriteLine(response.UserCode);
            Console.WriteLine("Or open the following URI : ");
            Console.WriteLine(response.VerificationUriComplete);
            Console.WriteLine("------------------------------------------");

            Console.WriteLine("Start polling...");

            while (true)
            {
                Thread.Sleep(response.Interval * 1000);

                var tokenResponse = DeviceFlowHelper.SendAccessTokenRequest("smart-tv", response.DeviceCode);

                if (tokenResponse.IsSuccessful)
                {
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine($"Access Token : {tokenResponse.Token.AccessToken}");
                    Console.WriteLine($"Token Type : {tokenResponse.Token.TokenType}");
                    Console.WriteLine($"Refresh Token : {tokenResponse.Token.RefreshToken}");
                    Console.WriteLine($"Expires In : {tokenResponse.Token.ExpiresIn}");
                    Console.WriteLine("------------------------------------------");
                    break;
                }
                Console.WriteLine(tokenResponse.Error);
            }
            Console.ReadLine();
        }
    }
}
