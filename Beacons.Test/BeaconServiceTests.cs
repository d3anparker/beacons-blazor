using Beacons.Services.Beacons;

namespace Beacons.Test
{
    public class BeaconServiceTests
    {
        public IBeaconService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new BeaconService();
        }

        [Test]
        public async Task ReturnsNullWhenNoMatchingIdFound()
        {
            var beacon = await _sut.GetByIdAsync(Guid.Empty);

            Assert.That(beacon, Is.Null);
        }
    }
}