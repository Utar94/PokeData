﻿using MediatR;
using PokeData.Domain.Resources;

namespace PokeData.Application.Resources.Commands;

internal class ImportSpeciesResourcesCommandHandler : IRequestHandler<ImportSpeciesResourcesCommand, Unit>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IResourceExtractor _resourceExtractor;
  private readonly IResourceRepository _resourceRepository;

  public ImportSpeciesResourcesCommandHandler(IApplicationContext applicationContext,
    IResourceExtractor resourceExtractor, IResourceRepository resourceRepository)
  {
    _applicationContext = applicationContext;
    _resourceExtractor = resourceExtractor;
    _resourceRepository = resourceRepository;
  }

  public async Task<Unit> Handle(ImportSpeciesResourcesCommand command, CancellationToken cancellationToken)
  {
    IEnumerable<Resource> resources = await _resourceExtractor.GetSpeciesAsync(command.Id, cancellationToken);

    ResourceId[] resourceIds = resources.Select(r => r.Id).Distinct().ToArray();
    Dictionary<ResourceId, ResourceAggregate> aggregates = (await _resourceRepository.LoadAsync(resourceIds, cancellationToken))
      .ToDictionary(r => r.Id, r => r);

    List<ResourceAggregate> savedAggregates = new(capacity: aggregates.Count);

    foreach (Resource resource in resources)
    {
      if (aggregates.TryGetValue(resource.Id, out ResourceAggregate? aggregate))
      {
        aggregate.Update(resource.Source, resource.Json, _applicationContext.ActorId);
        if (aggregate.HasChanges)
        {
          savedAggregates.Add(aggregate);
        }
      }
      else
      {
        aggregate = new(resource.Id, resource.Source, resource.Json, _applicationContext.ActorId);
        savedAggregates.Add(aggregate);

        aggregates[aggregate.Id] = aggregate;
      }
    }

    await _resourceRepository.SaveAsync(savedAggregates, cancellationToken);

    return Unit.Value;
  }
}