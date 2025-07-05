# CharityDonationSolution

This solution contains:
- **CharityDonationManager**: .NET Standard library for managing donations and generating exports.
- **CharityDonationManager.API**: ASP.NET Core Web API exposing CSV/PDF export endpoints.
- **CharityDonationManager.Tests**: xUnit tests for the library.

## Getting Started

1. Restore and build:
   ```
   dotnet restore
   dotnet build
   ```

2. Run the Web API:
   ```
   dotnet run --project src/CharityDonationManager.API/CharityDonationManager.API.csproj
   ```

3. Open Swagger at:
   https://localhost:<port>/swagger

## Features

- Generate CSV export of donations
- Generate PDF receipts via PdfSharpCore
- Clean layered architecture
