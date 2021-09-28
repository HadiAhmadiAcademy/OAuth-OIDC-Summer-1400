using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Diaries.Services
{
    public class TokenIntrospectionService
    {
        public static async Task<string> Introspect(string accessToken)
        {
            var requestBody = $"token={accessToken}";
            var client = new RestClient("https://localhost:5001/connect/introspect");
            var request = new RestRequest(Method.POST);
            var authorizationHeader = ToBase64("diaries-api:diaries-api-secret");
            request.AddHeader("Authorization", $"Basic {authorizationHeader}");
            request.AddParameter("application/x-www-form-urlencoded", requestBody, ParameterType.RequestBody);
            var result = client.Execute<dynamic>(request);
            return "";
        }

        private static string ToBase64(string value)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}