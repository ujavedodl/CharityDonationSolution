using CharityDonationManager.Interfaces;
using CharityDonationManager.Models;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CharityDonationManager.Services
{
    public class CsvExportService : IExportService
    {
        public async Task<string> ExportDonationsToCsvAsync(IEnumerable<Donation> donations)
        {
            var sb = new StringBuilder();
            sb.AppendLine("DonationId,DonorName,Amount,Date");
            foreach (var d in donations)
                sb.AppendLine($"{d.Id},{d.Donor.Name},{d.Amount},{d.Date:O}");
            var path = Path.Combine(Path.GetTempPath(), "donations.csv");
            await File.WriteAllTextAsync(path, sb.ToString());
            return path;
        }

        public async Task<string> GenerateReceiptsPdfAsync(IEnumerable<Donation> donations)
        {
            using var document = new PdfDocument();
            foreach (var d in donations)
            {
                var page = document.AddPage();
                var gfx  = XGraphics.FromPdfPage(page);
                var header = new XFont("Verdana", 18, XFontStyle.Bold);
                var body   = new XFont("Verdana", 12, XFontStyle.Regular);

                gfx.DrawString("Donation Receipt", header, XBrushes.Black,
                    new XRect(0, 20, page.Width, 30), XStringFormats.Center);

                gfx.DrawString($"Donor: {d.Donor.Name}", body, XBrushes.Black,
                    new XRect(40, 80, page.Width - 80, 20), XStringFormats.TopLeft);
                gfx.DrawString($"Email: {d.Donor.Email}", body, XBrushes.Black,
                    new XRect(40, 110, page.Width - 80, 20), XStringFormats.TopLeft);
                gfx.DrawString($"Campaign ID: {d.CampaignId}", body, XBrushes.Black,
                    new XRect(40, 140, page.Width - 80, 20), XStringFormats.TopLeft);
                gfx.DrawString($"Amount: ${d.Amount:F2}", body, XBrushes.Black,
                    new XRect(40, 170, page.Width - 80, 20), XStringFormats.TopLeft);
                gfx.DrawString($"Date: {d.Date:yyyy-MM-dd HH:mm}", body, XBrushes.Black,
                    new XRect(40, 200, page.Width - 80, 20), XStringFormats.TopLeft);
            }
            var pdfPath = Path.Combine(Path.GetTempPath(), $"receipts_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf");
            using var stream = File.Create(pdfPath);
            document.Save(stream);
            return pdfPath;
        }
    }
}
