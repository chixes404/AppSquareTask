namespace AppSquareTask.Infrastracture.Configuration
{
    public class JWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInDays { get; set; }
        public double RefreshTokenValidityInDays { get; set; } // Add this

    }
}
