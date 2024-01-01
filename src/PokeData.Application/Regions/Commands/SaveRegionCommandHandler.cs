using FluentValidation;
using Logitar;
using MediatR;
using PokeData.Application.Regions.Validators;
using PokeData.Contracts.Regions;
using PokeData.Domain.Regions;

namespace PokeData.Application.Regions.Commands;

internal class SaveRegionCommandHandler : IRequestHandler<SaveRegionCommand, Region>
{
  private readonly IRegionQuerier _regionQuerier;
  private readonly IRegionRepository _regionRepository;

  public SaveRegionCommandHandler(IRegionQuerier regionQuerier, IRegionRepository regionRepository)
  {
    _regionQuerier = regionQuerier;
    _regionRepository = regionRepository;
  }

  public async Task<Region> Handle(SaveRegionCommand command, CancellationToken cancellationToken)
  {
    SaveRegionPayload payload = command.Payload;
    new SaveRegionPayloadValidator().ValidateAndThrow(payload);

    RegionAggregate region = new()
    {
      Number = command.Number,
      UniqueName = payload.UniqueName.Trim(),
      DisplayName = payload.DisplayName?.CleanTrim()
    };
    await _regionRepository.SaveAsync(region, cancellationToken);

    return await _regionQuerier.ReadAsync(region.Number, cancellationToken)
      ?? throw new InvalidOperationException($"The region '#{region.Number}' could not be found.");
  }
}
