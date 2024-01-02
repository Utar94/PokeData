using Microsoft.AspNetCore.Mvc;
using PokeData.Constants;

namespace PokeData.Controllers;

[ApiController]
[Route("")]
public class IndexController : ControllerBase
{
  [HttpGet]
  public ActionResult Get() => Ok($"{Api.Title} v{Api.Version}");
}
