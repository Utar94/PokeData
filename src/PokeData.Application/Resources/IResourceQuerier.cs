namespace PokeData.Application.Resources;

public interface IResourceQuerier
{
  Task<Resource?> ReadBySourceAsync(string source, CancellationToken cancellationToken = default);
}
