public class Category
{
    public int id { get; set; }
    public string name { get; set; }
    public Icon icon { get; set; }
}

public class Icon
{
    public string prefix { get; set; }
    public string suffix { get; set; }
}

public class Chain
{
    public string id { get; set; }
    public string name { get; set; }
}

public class Geocode
{
    public double latitude { get; set; }
    public double longitude { get; set; }
}

public class Location
{
    public string address { get; set; }
    public string country { get; set; }
    public string cross_street { get; set; }
    public string formatted_address { get; set; }
    public string locality { get; set; }
    public string postcode { get; set; }
    public string region { get; set; }
}

public class Parent
{
    public string fsq_id { get; set; }
    public string name { get; set; }
}

public class RelatedPlaces
{
    public Parent parent { get; set; }
}

public class Geocodes
{
    public Geocode main { get; set; }
    public Geocode roof { get; set; }
    public Geocode drop_off { get; set; }
}

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

public class FoursquareResponse
{
    public FoursquarePlace[] results { get; set; }
}
