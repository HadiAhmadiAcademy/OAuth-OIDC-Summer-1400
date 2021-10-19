using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SmartTV.Model;

namespace SmartTV
{
    public static class DeviceFlowHelper
    {
        public static DeviceAuthorizationResponse SendDeviceAuthorizationRequest(string clientId)
        {
            var client = new RestClient("https://localhost:5001");

            var clientAuthentication = "smart-tv:smart-tv-secret".ToBase64();
            var requestBody = $"client_id={clientId}&scope=read-diaries offline_access";
            var request = new RestRequest("/connect/deviceauthorization",Method.POST);
            request.AddHeader("Content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", $"Basic {clientAuthentication}");
            request.AddParameter("application/x-www-form-urlencoded", requestBody, ParameterType.RequestBody);

            var response = client.Execute<DeviceAuthorizationResponse>(request);
            return response.Data;
        }

        public static DeviceFlowTokenResponse SendAccessTokenRequest(string clientId, string deviceCode)
        {
            var client = new RestClient("https://localhost:5001");

            var clientAuthentication = "smart-tv:smart-tv-secret".ToBase64();
            var requestBody = $"grant_type=urn%3Aietf%3Aparams%3Aoauth%3Agrant-type%3Adevice_code" +
                              $"&device_code={deviceCode}" +
                              $"&client_id={clientId}";

            var request = new RestRequest("/connect/token", Method.POST);
            request.AddHeader("Authorization", $"Basic {clientAuthentication}");
            request.AddHeader("Content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", requestBody, ParameterType.RequestBody);
            var response = client.Execute<dynamic>(request);

            var tokenResponse = new DeviceFlowTokenResponse();
            if (response.IsSuccessful)
            {
                tokenResponse.IsSuccessful = true;
                tokenResponse.Token = JsonConvert.DeserializeObject<TokenResponse>(response.Content);
            }
            else
            {
                tokenResponse.IsSuccessful = false;
                tokenResponse.Error = JsonConvert.DeserializeObject<JObject>(response.Content)["error"].ToString();
            }
            return tokenResponse;
        }
    }
}
