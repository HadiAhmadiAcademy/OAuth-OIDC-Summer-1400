namespace SmartTV.Model
{
    public class DeviceFlowTokenResponse
    {
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        public TokenResponse Token { get; set; }
    }
}