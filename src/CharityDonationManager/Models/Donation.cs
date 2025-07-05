using System;

namespace CharityDonationManager.Models
{
    public class Donation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Donor Donor { get; set; } = null!;
        public Guid CampaignId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
