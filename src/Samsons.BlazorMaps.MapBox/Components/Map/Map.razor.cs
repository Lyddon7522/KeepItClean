using System.IO.MemoryMappedFiles;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Samsons.BlazorMaps.MapBox.Models;

namespace Samsons.BlazorMaps.MapBox.Components.Map;

public partial class Map
{
    [Inject] public required IJSRuntime JSRuntime { get; set; }
    
    [Parameter]
    public MapOptions MapOptions { get; set; }

    private IJSObjectReference _mapReference { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _mapReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Samsons.BlazorMaps.MapBox/Map.razor.js");
        }

        await _mapReference.InvokeVoidAsync("createMapBoxMap", MapOptions);
    }
}
