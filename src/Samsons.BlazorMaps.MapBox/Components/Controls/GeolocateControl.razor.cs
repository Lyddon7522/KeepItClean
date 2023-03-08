using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Samsons.BlazorMaps.MapBox.Components.Controls;

public partial class GeolocateControl
{
    [Inject] public required IJSRuntime JSRuntime { get; set; }

    private IJSObjectReference _geolocatControl = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _geolocatControl = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Samsons.BlazorMaps.MapBox/GeolocateControl.razor.js");
        }

        await _geolocatControl.InvokeVoidAsync("addGeolocateControl");
    }
}