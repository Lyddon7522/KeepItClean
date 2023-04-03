using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Samsons.BlazorMaps.MapBox.Models;

namespace Samsons.BlazorMaps.MapBox.Components.Map;

public partial class Map
{
    [Inject] public required IJSRuntime JSRuntime { get; set; }
    [Parameter] public MapOptions? MapOptions { get; set; }
    [Parameter] public FlyToOptions? FlyToOptions { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private readonly string _id = $"mapbox-{Guid.NewGuid():N}";

    private bool _isLoaded;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            MapOptions ??= new MapOptions();

            MapOptions.Container = _id;

            await JSRuntime.InvokeVoidAsync("createMapBoxMap", MapOptions);

            if (FlyToOptions is not null)
            {
                await JSRuntime.InvokeVoidAsync("flyTo", FlyToOptions);
            }

            _isLoaded = true;

            StateHasChanged();
        }
    }
}