using System.Text;

namespace RopcFlowRaw
{
    public static class EncodingUtil
    {
        public static string ToBase64(string value)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}