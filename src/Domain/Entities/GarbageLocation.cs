using Domain.Enums;

namespace Domain.Entities
{
    public class GarbageLocation : BaseEntity
    {
        private GarbageLocation() { }

        public GarbageLocation(VehicleSize vehicleSize)
        {
            VehicleSize = vehicleSize;
        }

        public VehicleSize VehicleSize { get; private set; }
    }
}
