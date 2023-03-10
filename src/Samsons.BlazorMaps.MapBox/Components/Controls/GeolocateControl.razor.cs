using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Samsons.BlazorMaps.MapBox.Components.Controls;

public partial class GeolocateControl
{
    [Inject] public required IJSRuntime JSRuntime { get; set; }

    private IJSObjectReference _geolocatControl = null!;
    
    [Parameter] public RenderFragment ChildContent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {

            _geolocatControl = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Samsons.BlazorMaps.MapBox/Components/Controls/GeolocateControl.razor.js");
        

        await _geolocatControl.InvokeVoidAsync("addGeolocateControl");
    }
}