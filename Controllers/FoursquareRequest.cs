namespace Places.Controllers
{
    public class FoursquareRequest
    {
        public double latitude {get; set;}
        public double longitude {get; set;}
        public int radius {get; set;}
        public string? clientId {get; set;}
        public string? clientSecret {get; set;}
        public string? query {get; set;}
        public string? limit {get; set;}
    }
}
