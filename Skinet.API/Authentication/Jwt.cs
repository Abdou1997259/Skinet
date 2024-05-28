namespace Skinet.API.Authentication
{

    public class Jwt
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int DurationInMinutes { get; set; }
    }

}
