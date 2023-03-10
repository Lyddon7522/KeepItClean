using System.IO.MemoryMappedFiles;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Samsons.BlazorMaps.MapBox.Models;

namespace Samsons.BlazorMaps.MapBox.Components.Map;

public partial class Map
{
    [Inject] public required IJSRuntime JSRuntime { get; set; }
    [Parameter] public MapOptions? MapOptions { get; set; }
    private IJSObjectReference _mapReference { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }

    private readonly string _id = $"mapbox-{Guid.NewGuid():N}";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _mapReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Samsons.BlazorMaps.MapBox/Components/Map/Map.razor.js");
        }

        MapOptions ??= new MapOptions();

        MapOptions.Container = _id;

        await _mapReference.InvokeVoidAsync("createMapBoxMap", MapOptions);
    }
}
