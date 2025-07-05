using CharityDonationManager.Models;
using Xunit;

namespace CharityDonationManager.Tests.Models
{
    public class DonorTests
    {
        [Fact]
        public void NewDonor_HasEmptyDonations()
        {
            var d = new Donor { Name = "Alice", Email = "a@b.com" };
            Assert.Empty(d.Donations);
        }
    }
}
