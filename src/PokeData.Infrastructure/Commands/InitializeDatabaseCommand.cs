using MediatR;

namespace PokeData.Infrastructure.Commands;

public record InitializeDatabaseCommand : INotification;
