using MediatR;

namespace PokeData.Seeding.Commands;

internal record SeedRegionsCommand(string Path, Encoding Encoding) : INotification;
