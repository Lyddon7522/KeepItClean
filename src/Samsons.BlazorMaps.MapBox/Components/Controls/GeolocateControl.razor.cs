using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Samsons.BlazorMaps.MapBox.Components.Controls;

public partial class GeolocateControl
{
    [Inject] public required IJSRuntime JSRuntime { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await JSRuntime.InvokeVoidAsync("addGeolocateControl");
    }
}