namespace PokeData.Infrastructure;

public class ResourceDeserializationException : Exception
{
  public HttpResponseDetail ResponseDetail
  {
    get => (HttpResponseDetail)Data[nameof(ResponseDetail)]!;
    private set => Data[nameof(ResponseDetail)] = value;
  }

  public ResourceDeserializationException(HttpResponseDetail responseDetail, Exception innerException)
    : base("The resource deserialization failed.", innerException)
  {
    ResponseDetail = responseDetail;
  }
}
