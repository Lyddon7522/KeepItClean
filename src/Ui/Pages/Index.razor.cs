using Domain.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RESTFulSense.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ui.Models;

namespace Ui.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] private IJSRuntime JsRuntime { get; set; }
        [Inject] private IRESTFulApiFactoryClient HttpClient { get; set; }

        private readonly IEnumerable<VehicleSize> VehicleSizes = VehicleSize.List;

        private readonly GarbageLocationModel Model = new GarbageLocationModel();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JsRuntime.InvokeVoidAsync("initialize", null);
        }

        private async Task HandleValidSubmit()
        {
            await HttpClient.PostContentAsync("/api/garbagelocations", Model);
        }
    }
}
