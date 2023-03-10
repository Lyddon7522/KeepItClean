using Samsons.BlazorMaps.MapBox.Models;

namespace KeepItClean.Client.Pages;

public partial class Index
{
    private MapOptions _options = new MapOptions()
    {
        Center = new LngLatLike { Longitude = -93.6250, Latitude = 41.5868 },
        Zoom = 10,
        Style = "mapbox://styles/mapbox/satellite-streets-v12"
    };
}