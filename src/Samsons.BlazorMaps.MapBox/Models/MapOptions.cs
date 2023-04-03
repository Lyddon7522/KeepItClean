namespace Samsons.BlazorMaps.MapBox.Models;

public class MapOptions
{
    public LngLatLike Center { get; set; } = new LngLatLike { Longitude = 0, Latitude = 0 };
    public string? Container { get; set; }
    public string? Style { get; set; }
    public int Zoom { get; set; } = 0;
}

public class LngLatLike
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}

