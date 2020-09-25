using Api.Models;
using Application.GarbageLocations.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarbageLocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GarbageLocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Post(GarbageLocationModel model)
        {
            await _mediator.Send(new AddGarbageLocationCommand(model.VehicleSizeName));

            return Ok();
        }
    }
}
