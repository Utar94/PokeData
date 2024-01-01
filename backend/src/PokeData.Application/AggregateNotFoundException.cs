using Logitar;

namespace PokeData.Application;

public class AggregateNotFoundException : Exception
{
  public const string ErrorMessage = "The specified aggregate could not be found.";

  public string TypeName
  {
    get => (string)Data[nameof(TypeName)]!;
    private set => Data[nameof(TypeName)] = value;
  }
  public string AggregateId
  {
    get => (string)Data[nameof(AggregateId)]!;
    private set => Data[nameof(AggregateId)] = value;
  }
  public string? PropertyName
  {
    get => (string?)Data[nameof(PropertyName)];
    private set => Data[nameof(PropertyName)] = value;
  }

  public AggregateNotFoundException(Type type, string aggregateId, string? propertyName = null) : base(BuildMessage(type, aggregateId, propertyName))
  {
    TypeName = type.GetNamespaceQualifiedName();
    AggregateId = aggregateId;
    PropertyName = propertyName;
  }

  private static string BuildMessage(Type type, string aggregateId, string? propertyName) => new ErrorMessageBuilder(ErrorMessage)
    .AddData(nameof(TypeName), type.GetNamespaceQualifiedName())
    .AddData(nameof(AggregateId), aggregateId)
    .AddData(nameof(PropertyName), propertyName ?? "<null>")
    .Build();
}

public class AggregateNotFoundException<T> : AggregateNotFoundException
{
  public AggregateNotFoundException(string aggregateId, string? propertyName = null) : base(typeof(T), aggregateId, propertyName)
  {
  }
}
