using KeepItClean.Server.Infrastructure.Models;
using KeepItClean.Shared.Features;

namespace KeepItClean.Tests.Features;

public class LocationTests : IntegrationTestBase
{
    private const string _endpoint = "/api/locations";

    [Test]
    public async Task GivenAValidLocation_ThenIsSaved()
    {
        var application = GetUnauthenticatedApplication();

        var mockLocation = new AutoFaker<AddLocationRequest>().Generate();

        await application.CreateClient().PostAsJsonAsync(_endpoint, mockLocation);

        var location = await FirstOrDefaultAsync<Location>();
        location.Should().NotBeNull();
        location!.Latitude.Should().Be(mockLocation.Latitude);
        location.Longitude.Should().Be(mockLocation.Longitude);
        location.Name.Should().Be(mockLocation.Name);
        location.Id.Should().NotBe(Guid.Empty);
    }
}