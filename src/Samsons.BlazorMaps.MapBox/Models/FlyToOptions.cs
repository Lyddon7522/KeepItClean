namespace Samsons.BlazorMaps.MapBox.Models;

public class FlyToOptions
{
    public LngLatLike Center { get; set; } = new LngLatLike { Longitude = 0, Latitude = 0 };
    public double Curve { get; set; }
    public double MaxDuration { get; set; }
    public double Zoom { get; set; }
    public double Speed { get; set; }
}