﻿namespace PokeData.Contracts.Errors;

public record ErrorData
{
  public string Key { get; set; }
  public string? Value { get; set; }

  public ErrorData() : this(string.Empty)
  {
  }
  public ErrorData(string key, object? value = null)
  {
    Key = key;
    Value = value?.ToString();
  }
}
