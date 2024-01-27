using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;


using Pebbles.Models;
using Pebbles.Services;
using Pebbles.Repositories;

[ApiController]
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
      try
      {
        var store = await _storeService.GetPatientStoreAsync(patientId);
        return Ok(JsonConvert.SerializeObject(store));
      }
      catch (Exception ex)
      {
        return StatusCode(500, "Internal server error.");
      }
    }

    [HttpGet("{patientId}/byprice")]
    public async Task<IActionResult> GetPatientStoreByPriceAsync(Guid patientId)
    {
      try
      {
        var store = await _storeService.GetPatientStoreByPriceAsync(patientId);
        return Ok(JsonConvert.SerializeObject(store));
      }
      catch (Exception ex)
      {
        return StatusCode(500, "Internal server error.");
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
        return StatusCode(500, "Internal server error.");
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
            return StatusCode(500, "Internal server error.");
        }
    }
}
