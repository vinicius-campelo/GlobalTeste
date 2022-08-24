namespace WebSemBanco.Data.Utils
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; } = 5;
        public int Minutes { get; set; } = 2;
    }
}
