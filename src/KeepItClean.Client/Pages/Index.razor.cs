using Samsons.BlazorMaps.MapBox.Models;

namespace KeepItClean.Client.Pages;

public partial class Index
{
    private static readonly LngLatLike _coordinates = new LngLatLike { Longitude = -93.6250, Latitude = 41.5868 }
        ; 
    private MapOptions _options = new MapOptions()
    {
        Center = _coordinates,
        Zoom = 10,
        Style = "mapbox://styles/mapbox/satellite-streets-v12"
    };

    private FlyToOptions _flyToOptions = new FlyToOptions()
    {
        Center = _coordinates,
        Zoom = 12,
        Speed = 1.2,
        Curve = 1
    };
}