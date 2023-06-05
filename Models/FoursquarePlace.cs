namespace Places.Models;
public class FoursquarePlace
{
    public string fsq_id { get; set; }
    public Category[] categories { get; set; }
    public Chain[] chains { get; set; }
    public int distance { get; set; }
    public Geocodes geocodes { get; set; }
    public string link { get; set; }
    public Location location { get; set; }
    public string name { get; set; }
    public RelatedPlaces related_places { get; set; }
    public string timezone { get; set; }
}