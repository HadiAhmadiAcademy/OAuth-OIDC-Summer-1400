using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Diaries.Services
{
    public static class RequestExtensions
    {
        public static string GetAccessToken(this HttpRequest request)
        {
            var header = request.Headers["authorization"].First();
            return header.Split(" ")[1];
        }
    }
}
