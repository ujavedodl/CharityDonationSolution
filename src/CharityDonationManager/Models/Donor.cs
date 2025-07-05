using System;
using System.Collections.Generic;

namespace CharityDonationManager.Models
{
    public class Donor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Donation> Donations { get; set; } = new List<Donation>();

    }
}
