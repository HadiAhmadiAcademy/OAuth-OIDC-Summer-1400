using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace RopcFlowRaw
{
    public class TokenRevocationService
    {
        public static HttpStatusCode Revoke(string accessToken)
        {
            var requestBody = $"token={accessToken}";

            var client = new RestClient("https://localhost:5001/connect/revocation");
            var request = new RestRequest(Method.POST);
            var authorization = EncodingUtil.ToBase64("console-ropc-raw:console-ropc-raw-secret");
            request.AddHeader("Authorization", $"Basic {authorization}");
            request.AddParameter("application/x-www-form-urlencoded", requestBody, ParameterType.RequestBody);
            var response = client.Execute(request);
            return response.StatusCode;
        }
       
    }
}
