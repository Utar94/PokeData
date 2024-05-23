using CsvHelper;
using Logitar;
using MediatR;
using PokeData.Contracts.Regions;
using PokeData.Seeding.Models;

namespace PokeData.Seeding.Commands;

internal class SeedRegionsCommandHandler : INotificationHandler<SeedRegionsCommand>
{
  private readonly ILogger<SeedRegionsCommandHandler> _logger;
  private readonly IRegionService _regionService;

  public SeedRegionsCommandHandler(ILogger<SeedRegionsCommandHandler> logger, IRegionService regionService)
  {
    _logger = logger;
    _regionService = regionService;
  }

  public async Task Handle(SeedRegionsCommand command, CancellationToken cancellationToken)
  {
    using StreamReader reader = new(command.Path, command.Encoding);
    using CsvReader csv = new(reader, CultureInfo.InvariantCulture);

    RegionData[] records = csv.GetRecords<RegionData>().ToArray();
    _logger.LogInformation("Found {Count} regions to seed.", records.Length);

    foreach (RegionData record in records)
    {
      SaveRegionPayload payload = new()
      {
        UniqueName = record.UniqueName.Trim(),
        DisplayName = record.DisplayName?.CleanTrim()
      };
      Region region = await _regionService.SaveAsync(record.Number, payload, cancellationToken);
      _logger.LogInformation("Seeded {Region}.", region);
    }
  }
}
