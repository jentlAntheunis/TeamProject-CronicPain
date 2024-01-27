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
  private readonly IQuestionnaireService _questionnaireService;

  public PatientController(
      IPatientService patientService,
      IConfiguration configuration,
      IQuestionnaireService questionnaireService
      )
  {
    _configuration = configuration;
    _patientService = patientService;
    _questionnaireService = questionnaireService;
  }

  [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] //only authenticated users can access this controller
  [HttpGet("{patientId}")]
  public async Task<IActionResult> GetPatientAsync(Guid patientId)
  {
    try
    {
      var patient = await _patientService.GetPatientByIdAsync(patientId);
      if (patient == null)
      {
        return StatusCode(500);
      }
      return Ok(JsonConvert.SerializeObject(patient));
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }

  }

  [HttpGet("{patientId}/details")]
  public async Task<IActionResult> GetPatientDetailsByIdAsync(Guid patientId)
  {
    try
    {
      var patient = await _patientService.GetPatientDetailsByIdAsync(patientId);
      if (patient == null)
      {
        return StatusCode(500);
      }
      return Ok(JsonConvert.SerializeObject(patient));
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }

  }

  [HttpGet("{patientId}/movementsessions")]
  public async Task<IActionResult> GetMovementSessionsAsync(Guid patientId)
  {
    try
    {
      var movementSessions = await _patientService.GetMovementSessionsAsync(patientId);
      return Ok(JsonConvert.SerializeObject(movementSessions));
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }

  }

  [HttpPost("{patientId}/movementsessions")]
  public async Task<IActionResult> AddMovementSessionAsync(Guid patientId, [FromBody] MovementSession movementSession)
  {
    try
    {
      var movementSessionId = await _patientService.AddMovementSessionAsync(patientId, movementSession);
      return Ok(JsonConvert.SerializeObject(movementSessionId));
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }

  }

  [HttpGet("{patientId}/movementsuggestions")]
  public async Task<IActionResult> GetMovementSuggestionsAsync(Guid patientId)
  {
    try
    {
      var movementSuggestions = await _patientService.GetMovementSuggestionsAsync(patientId);
      return Ok(JsonConvert.SerializeObject(movementSuggestions));
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }

  }

  [HttpGet("{patientId}/pebblesmood")]
  public async Task<IActionResult> GetPebblesMoodAsync(Guid patientId)
  {
    try
    {
      var pebblesMood = await _patientService.GetPebblesMoodAsync(patientId);
      return Ok(JsonConvert.SerializeObject(pebblesMood));
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }

  }

  [HttpPut("{patientId}/addcoins/{amount}")]
  public async Task<IActionResult> AddCoinsAsync(Guid patientId, int amount)
  {
    try
    {
      await _patientService.AddCoinsAsync(patientId, amount);
      return Ok();
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }

  }

  [HttpPut("{patientId}/checkstreak")]
  public async Task<IActionResult> CheckStreakAsync(Guid patientId)
  {
    try
    {
      await _patientService.CheckStreakAsync(patientId);
      return Ok();
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }

  }

  [HttpPut("{patientId}/addstreak")]
  public async Task<IActionResult> AddStreakAsync(Guid patientId)
  {
    try
    {
      await _patientService.AddStreakAsync(patientId);
      return Ok();
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }

  }

  [HttpGet("{patientId}/movementtimeweek")]
  public async Task<IActionResult> GetMovementTimeWeekAsync(Guid patientId)
  {
    try
    {
      var movementTimeWeek = await _patientService.GetMovementTimeWeekAsync(patientId);
      return Ok(JsonConvert.SerializeObject(movementTimeWeek));
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }

  }

  [HttpGet("{patientId}/streakhistory")]
  public async Task<IActionResult> GetStreakHistoryAsync(Guid patientId)
  {
    try
    {
      var streakHistory = await _patientService.GetStreakHistoryAsync(patientId);
      return Ok(JsonConvert.SerializeObject(streakHistory));
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }

  }

  [HttpGet("{patientId}/painhistory")]
  public async Task<IActionResult> GetPainHistoryAsync(Guid patientId)
  {
    try
    {
      var painHistory = await _patientService.GetPainHistoryAsync(patientId);
      return Ok(JsonConvert.SerializeObject(painHistory));
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }

  }

  [HttpGet("{patientId}/questionnaires")]
  public async Task<IActionResult> GetAllQuestionnairesFromPatientAsync(Guid patientId)
  {
    try
    {
      var categories = new List<string> { "beweging", "bonus" };
      var questionnaires = await _questionnaireService.GetQuestionnairesWithDetailsByPatientIdAsync(patientId, categories);
      return Ok(questionnaires);
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal Server Error.");
    }
  }
}


