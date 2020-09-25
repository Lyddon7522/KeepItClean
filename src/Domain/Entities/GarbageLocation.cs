namespace Domain.Entities
{
    public class GarbageLocation : BaseEntity
    {
        private GarbageLocation() { }

        public GarbageLocation(string vehicleSizeName)
        {
            VehicleSizeName = vehicleSizeName;
        }

        public string VehicleSizeName { get; set; }
    }
}
