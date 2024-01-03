using KeepItClean.Server.Infrastructure.Models;
using KeepItClean.Web.Features;
using KeepItClean.Web.Tests;

namespace KeepItClean.Tests.Features;

public class LocationTests : IntegrationTestBase
{
    [Test]
    public async Task GivenAValidLocation_ThenIsSaved()
    {
        var mockLocation = Faker.Create<AddLocationCommand>();

        await SendAsync(mockLocation);

        var location = await FirstOrDefaultAsync<Location>();
        location.Should().NotBeNull();
        location!.Latitude.Should().Be(mockLocation.Latitude);
        location.Longitude.Should().Be(mockLocation.Longitude);
        location.Name.Should().Be(mockLocation.Name);
        location.Id.Should().NotBe(Guid.Empty);
    }
}