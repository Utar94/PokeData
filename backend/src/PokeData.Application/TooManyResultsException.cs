﻿using Logitar;
using PokeData.Contracts.Errors;

namespace PokeData.Application;

public class TooManyResultsException : Exception
{
  public const string ErrorMessage = "Too many results were found.";

  public string TypeName
  {
    get => (string)Data[nameof(TypeName)]!;
    private set => Data[nameof(TypeName)] = value;
  }
  public int ExpectedCount
  {
    get => (int)Data[nameof(ExpectedCount)]!;
    private set => Data[nameof(ExpectedCount)] = value;
  }
  public int ActualCount
  {
    get => (int)Data[nameof(ActualCount)]!;
    private set => Data[nameof(ActualCount)] = value;
  }

  public Error Error => new(this.GetErrorCode(), ErrorMessage, new ErrorData[]
  {
    new(nameof(ExpectedCount), ExpectedCount),
    new(nameof(ActualCount), ActualCount)
  });

  public TooManyResultsException(Type type, int expectedCount, int actualCount) : base(BuildMessage(type, expectedCount, actualCount))
  {
    TypeName = type.GetNamespaceQualifiedName();
    ExpectedCount = expectedCount;
    ActualCount = actualCount;
  }

  private static string BuildMessage(Type type, int expectedCount, int actualCount) => new ErrorMessageBuilder(ErrorMessage)
    .AddData(nameof(TypeName), type.GetNamespaceQualifiedName())
    .AddData(nameof(ExpectedCount), expectedCount)
    .AddData(nameof(ActualCount), actualCount)
    .Build();
}

public class TooManyResultsException<T> : TooManyResultsException
{
  public TooManyResultsException(int expectedCount, int actualCount) : base(typeof(T), expectedCount, actualCount)
  {
  }
}
