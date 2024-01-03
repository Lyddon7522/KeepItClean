using KeepItClean.Server.Infrastructure;
using KeepItClean.Server.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace KeepItClean.Web.Features;

public partial class Home
{
    [Inject] public required IMediator Mediator { get; set; }
    [CascadingParameter] public required HttpContext HttpContext { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Mediator.Send(new AddLocationCommand(10, 10, ""), HttpContext.RequestAborted);
    }
}


public record AddLocationCommand(decimal Longitude, decimal Latitude, string Name) : IRequest;

public class AddLocationHandler(IRepository<Location> repository) : IRequestHandler<AddLocationCommand>
{
    public async Task Handle(AddLocationCommand command, CancellationToken cancellationToken)
    {
        var location = new Location { Longitude = command.Longitude, Latitude = command.Latitude, Name = command.Name };
        await repository.AddAsync(location, cancellationToken);
    }
}
