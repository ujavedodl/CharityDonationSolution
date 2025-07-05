using CharityDonationManager.Interfaces;
using CharityDonationManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityDonationManager.API.Controllers
{
    public record DonationDto(
        Guid Id,
        Guid CampaignId,
        decimal Amount,
        DateTime Date,
        Guid DonorId,
        string DonorName,
        string DonorEmail
    );

    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IExportService _exportService;

        public ReportsController(IExportService exportService)
            => _exportService = exportService;

        [HttpPost("pdf")]
        public async Task<IActionResult> GeneratePdf([FromBody] List<DonationDto> dtos)
        {
            var donations = dtos.Select(d => new Donation
            {
                Id = d.Id,
                CampaignId = d.CampaignId,
                Amount = d.Amount,
                Date = d.Date,
                Donor = new Donor { Id = d.DonorId, Name = d.DonorName, Email = d.DonorEmail }
            });

            var path = await _exportService.GenerateReceiptsPdfAsync(donations);
            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(bytes, "application/pdf", Path.GetFileName(path));
        }
        [HttpPost("csv")]
        public async Task<IActionResult> GenerateCsv([FromBody] List<DonationDto> dtos)
        {
            // Map DTO → domain models
            var donations = dtos.Select(d => new Donation
            {
                Id = d.Id,
                CampaignId = d.CampaignId,
                Amount = d.Amount,
                Date = d.Date,
                Donor = new Donor { Id = d.DonorId, Name = d.DonorName, Email = d.DonorEmail }
            });

            // Generate CSV
            var path = await _exportService.ExportDonationsToCsvAsync(donations);
            var csv = await System.IO.File.ReadAllTextAsync(path);

            // Return as text/csv download
            return File(Encoding.UTF8.GetBytes(csv), "text/csv", Path.GetFileName(path));
        }

    }
}
