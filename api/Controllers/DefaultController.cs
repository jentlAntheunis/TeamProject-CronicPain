using Microsoft.AspNetCore.Mvc;

using Pebbles.Services;


namespace Pebbles.Controllers.V1
{
  [ApiController]
  [ApiVersion("1.0")]
  [Route("/")]
  public class DefaultController : ControllerBase
  {
    private readonly IConfiguration _configuration;

    public DefaultController(IAnswerService answerService, IConfiguration configuration, IOptionService optionService)
    {
      _configuration = configuration;
    }

    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
      return Ok("Ja hoor, de backend werkt");
    }

  }
}

namespace Pebbles.Controllers.V2
{
  [ApiController]
  [ApiVersion("2.0")]
  [Route("/")]
  public class DefaultController : ControllerBase
  {

    public DefaultController() { }

    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
      return Ok("Ja hoor, de backend werkt");
    }
  }
}
