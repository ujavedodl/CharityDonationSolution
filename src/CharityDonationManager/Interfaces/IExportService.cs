using System.Collections.Generic;
using System.Threading.Tasks;
using CharityDonationManager.Models;

namespace CharityDonationManager.Interfaces
{
    public interface IExportService
    {
        Task<string> ExportDonationsToCsvAsync(IEnumerable<Donation> donations);
        Task<string> GenerateReceiptsPdfAsync(IEnumerable<Donation> donations);
    }
}
