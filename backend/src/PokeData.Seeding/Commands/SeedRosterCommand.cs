using MediatR;

namespace PokeData.Seeding.Commands;

internal record SeedRosterCommand(string Path, Encoding Encoding) : INotification;
