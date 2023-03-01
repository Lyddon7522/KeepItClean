using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Samsons.BlazorMaps.MapBox;

public partial class Map
{
    [Inject] public required IJSRuntime JSRuntime { get; set; }

    private IJSObjectReference _mapModule = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _mapModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Samsons.BlazorMaps.MapBox/Map.razor.js");
        }

        await _mapModule.InvokeVoidAsync("getGeolocation");
    }
}
