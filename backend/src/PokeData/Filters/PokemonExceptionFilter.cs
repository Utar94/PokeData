using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PokeData.Application;

namespace PokeData.Filters;

internal class PokemonExceptionFilter : ExceptionFilterAttribute
{
  private readonly Dictionary<Type, Func<ExceptionContext, IActionResult>> _handlers = new()
  {
    [typeof(ValidationException)] = HandleValidationException
  };

  public override void OnException(ExceptionContext context)
  {
    if (_handlers.TryGetValue(context.Exception.GetType(), out Func<ExceptionContext, IActionResult>? handler))
    {
      context.Result = handler(context);
      context.ExceptionHandled = true;
    }
    else if (context.Exception is AggregateNotFoundException aggregateNotFound)
    {
      context.Result = new NotFoundObjectResult(aggregateNotFound.Error);
      context.ExceptionHandled = true;
    }
    else if (context.Exception is TooManyResultsException tooManyResults)
    {
      context.Result = new BadRequestObjectResult(tooManyResults.Error);
      context.ExceptionHandled = true;
    }
    else
    {
      base.OnException(context);
    }
  }

  private static BadRequestObjectResult HandleValidationException(ExceptionContext context)
  {
    ValidationException exception = (ValidationException)context.Exception;
    return new BadRequestObjectResult(new { exception.Errors });
  }
}
