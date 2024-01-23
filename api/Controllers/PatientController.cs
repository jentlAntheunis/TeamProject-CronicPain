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
[Route("patients")]
[Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller

public class PatientController : ControllerBase
{
  private readonly IConfiguration _configuration;
  private readonly IPatientService _patientService;

  public PatientController(
      IPatientService patientService,
      IConfiguration configuration
      )
  {
    _configuration = configuration;
    _patientService = patientService;
  }

  [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] //only authenticated users can access this controller
  [HttpGet("{patientId}")]
  public async Task<IActionResult> GetPatientAsync(Guid patientId)
  {
    var patient = await _patientService.GetPatientByIdAsync(patientId);
    if (patient == null)
    {
      return NotFound();
    }
    return Ok(JsonConvert.SerializeObject(patient));
  }

  [HttpGet("{patientId}/details")]
  public async Task<IActionResult> GetPatientDetailsByIdAsync(Guid patientId)
  {
    var patient = await _patientService.GetPatientDetailsByIdAsync(patientId);
    return Ok(JsonConvert.SerializeObject(patient));
  }

  [HttpGet("{patientId}/movementsessions")]
  public async Task<IActionResult> GetMovementSessionsAsync(Guid patientId)
  {
    var movementSessions = await _patientService.GetMovementSessionsAsync(patientId);
    return Ok(JsonConvert.SerializeObject(movementSessions));
  }

  [HttpPost("{patientId}/movementsessions")]
  public async Task<IActionResult> AddMovementSessionAsync(Guid patientId, [FromBody] MovementSession movementSession)
  {
    var movementSessionId = await _patientService.AddMovementSessionAsync(patientId, movementSession);
    return Ok(JsonConvert.SerializeObject(movementSessionId));
  }

  [HttpGet("{patientId}/movementsuggestions")]
  public async Task<IActionResult> GetMovementSuggestionsAsync(Guid patientId)
  {
    var movementSuggestions = await _patientService.GetMovementSuggestionsAsync(patientId);
    return Ok(JsonConvert.SerializeObject(movementSuggestions));
  }

  [HttpGet("{patientId}/pebblesmood")]
  public async Task<IActionResult> GetPebblesMoodAsync(Guid patientId)
  {
    var pebblesMood = await _patientService.GetPebblesMoodAsync(patientId);
    return BadRequest("not implemented");
  }

  [HttpPut("{patientId}/addcoins/{amount}")]
  public async Task<IActionResult> AddCoinsAsync(Guid patientId, int amount)
  {
    await _patientService.AddCoinsAsync(patientId, amount);
    return Ok();
  }

  [HttpPut("{patientId}/checkstreak")]
  public async Task<IActionResult> CheckStreakAsync(Guid patientId)
  {
    await _patientService.CheckStreakAsync(patientId);
    return Ok();
  }
}
