using Ardalis.SmartEnum;

namespace Domain.Enums
{
    public class VehicleSize : SmartEnum<VehicleSize>
    {
        public VehicleSize(string name, int value) : base(name, value)
        {

        }

        public static VehicleSize Car = new VehicleSize("Car", 1);
        public static VehicleSize PickupTruck = new VehicleSize("Pickup Truck", 2);
        public static VehicleSize DumpTruck = new VehicleSize("Dump Truck", 3);
    }
}
