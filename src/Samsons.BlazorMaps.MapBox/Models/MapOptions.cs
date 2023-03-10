namespace Samsons.BlazorMaps.MapBox.Models;

public class MapOptions
{
    
    /*public string AccessToken { get; set; } = null;
    public bool Antialias { get; set; } = false;
    public bool AttributionControl { get; set; } = true;
    public double Bearing { get; set; } = 0;
    public double BearingSnap { get; set; } = 7;
    public LngLatBoundsLike Bounds { get; set; } = null;
    public bool BoxZoom { get; set; } = true;*/
    public LngLatLike Center { get; set; } = new LngLatLike { Longitude = 0, Latitude = 0 };
    /*//public LngLatLike Center { get; set; } = new LngLatLike { Longitude = 0, Latitude = 0 };
    public double ClickTolerance { get; set; } = 3;
    public bool CollectResourceTiming { get; set; } = false;*/
    public string? Container { get; set; }
    /*public bool? CooperativeGestures { get; set; } = null;
    public bool CrossSourceCollisions { get; set; } = true;
    public List<string> CustomAttribution { get; set; } = null;
    public bool DoubleClickZoom { get; set; } = true;
    public bool DragPan { get; set; } = true;
    public bool DragRotate { get; set; } = true;
    public int FadeDuration { get; set; } = 300;
    public bool FailIfMajorPerformanceCaveat { get; set; } = false;
    public object FitBoundsOptions { get; set; } = null;
    public bool Hash { get; set; } = false;
    public bool Interactive { get; set; } = true;
    public bool Keyboard { get; set; } = true;
    public Language Language { get; set; } = null;
    public object Locale { get; set; }
    public string LocalFontFamily { get; set; } = "false";
    public string LocalIdeographFontFamily { get; set; } = "sans-serif";
    public string LogoPosition { get; set; } = "bottom-left";
    public LngLatBoundsLike MaxBounds { get; set; }
    public int MaxPitch { get; set; } = 85;
    public int? MaxTileCacheSize { get; set; }
    public int MaxZoom { get; set; } = 22;
    public int MinPitch { get; set; }
    public int? MinTileCacheSize { get; set; }
    public int MinZoom { get; set; }
    public bool OptimizeForTerrain { get; set; } = true;
    public bool PerformanceMetricsCollection { get; set; } = true;
    public int Pitch { get; set; }
    public bool PitchWithRotate { get; set; } = true;
    public bool PreserveDrawingBuffer { get; set; } = false;
    //public ProjectionSpecification projection { get; set; } = "mercator";
    public bool RefreshExpiredTiles { get; set; } = true;
    public bool RenderWorldCopies { get; set; } = true;
    public bool ScrollZoom { get; set; } = true;*/
    public string? Style { get; set; }
    /*public bool TestMode { get; set; } = false;
    public bool TouchPitch { get; set; } = true;
    public bool TouchZoomRotate { get; set; } = true;
    public bool TrackResize { get; set; } = true;
    //public Func<string, RequestParameters> transformRequest { get; set; } = null;
    public bool UseWebGl2 { get; set; } = false;
    public string? Worldview { get; set; } = null;*/
    public int Zoom { get; set; } = 0;
}

public class LngLatLike
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}

/*public class LngLatBoundsLike
{
    public LngLatLike sw { get; set; }
    public LngLatLike ne { get; set; }
}*/

/*public class HTMLElementOrString
{
    public string id { get; set; } = null;
    public HTMLElement element { get; set; } = null;
}*/

/*public class Language
{
    public string? Auto { get; set; } = null;
    public List<string>? Languages { get; set; } = null;
}*/
