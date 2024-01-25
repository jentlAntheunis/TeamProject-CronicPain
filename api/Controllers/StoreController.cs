using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

using Pebbles.Services;

namespace Pebbles.Controllers.V1
{
  [ApiController]
  [ApiVersion("1.0")]
  [Route("store")]
  [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller

  public class StoreController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly IStoreService _storeService;

    public StoreController(
        IStoreService storeService,
        IConfiguration configuration
        )
    {
      _configuration = configuration;
      _storeService = storeService;
    }

    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetPatientStoreAsync(Guid patientId)
    {
      var store = await _storeService.GetPatientStoreAsync(patientId);
      return Ok(JsonConvert.SerializeObject(store));
    }

    [HttpPut("{patientId}/buy/{colorId}")]
    public async Task<IActionResult> BuyColorAsync(Guid patientId, Guid colorId)
    {
      await _storeService.PurchaseColorAsync(patientId, colorId);
      return Ok();
    }

    [HttpPut("{patientId}/use/{colorId}")]
    public async Task<IActionResult> UseColorAsync(Guid patientId, Guid colorId)
    {
      await _storeService.UseColorAsync(patientId, colorId);
      return Ok();
    }
  }
}

namespace Pebbles.Controllers.V2
{
  [ApiController]
  [ApiVersion("2.0")]
  [Route("store")]
  [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller

  public class StoreController : ControllerBase
  {
    private readonly IStoreService _storeService;

    public StoreController(IStoreService storeService)
    {
      _storeService = storeService;
    }

    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetPatientStoreAsync(Guid patientId)
    {
      try
      {
        var store = await _storeService.GetPatientStoreAsync(patientId);
        return Ok(JsonConvert.SerializeObject(store));
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return StatusCode(500, "Internal Server Error");
      }
    }

    [HttpPut("{patientId}/buy/{colorId}")]
    public async Task<IActionResult> BuyColorAsync(Guid patientId, Guid colorId)
    {
      try
      {
        await _storeService.PurchaseColorAsync(patientId, colorId);
        return Ok();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return StatusCode(500, "Internal Server Error");
      }
    }

    [HttpPut("{patientId}/use/{colorId}")]
    public async Task<IActionResult> UseColorAsync(Guid patientId, Guid colorId)
    {
      try
      {
        await _storeService.UseColorAsync(patientId, colorId);
        return Ok();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return StatusCode(500, "Internal Server Error");
      }
    }
  }
}
