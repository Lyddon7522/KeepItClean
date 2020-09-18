using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.GarbageLocations.Commands
{
    public class AddGarbageLocationCommand : IRequest<Unit>
    {
        private VehicleSize VehicleSize { get; }

        public AddGarbageLocationCommand(VehicleSize vehicleSize)
        {
            VehicleSize = vehicleSize;
        }

        internal sealed class Handler : IRequestHandler<AddGarbageLocationCommand, Unit>
        {
            private readonly IKeepItCleanDbContext _dbContext;

            public Handler(IKeepItCleanDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Unit> Handle(AddGarbageLocationCommand request, CancellationToken cancellationToken = default)
            {
                _dbContext.GarbageLocations.Add(new GarbageLocation(request.VehicleSize));
                await _dbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
