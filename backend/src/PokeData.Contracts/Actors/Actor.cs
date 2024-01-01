namespace PokeData.Contracts.Actors;

public class Actor
{
  public string Id { get; set; } = "SYSTEM";
  public ActorType Type { get; set; }
  public bool IsDeleted { get; set; }

  public string DisplayName { get; set; } = "System";
  public string? EmailAddress { get; set; }
  public string? PictureUrl { get; set; }

  public override bool Equals(object? obj) => obj is Actor actor && actor.Id == Id;
  public override int GetHashCode() => Id.GetHashCode();
  public override string ToString() => $"{DisplayName} ({Type}.Id={Id})";
}
