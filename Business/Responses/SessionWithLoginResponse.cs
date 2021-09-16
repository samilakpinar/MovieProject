namespace Business.Responses
{
    public class SessionWithLoginResponse
    {
        public bool success { get; set; }
        public string expires_at { get; set; }
        public string request_token { get; set; }
    }
}
