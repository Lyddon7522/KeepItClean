using Amazon.DynamoDBv2.DataModel;

namespace KeepItClean.Server.Infrastructure.Models;

[DynamoDBTable("Locations")]
public class Location
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required decimal Latitude { get; set; }
    public required decimal Longitude { get; set; }
}
