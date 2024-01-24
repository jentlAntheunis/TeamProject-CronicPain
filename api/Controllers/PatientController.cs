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

namespace Pebbles.Controllers.V1
{
  [ApiController]
  [ApiVersion("1.0")]
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
      return Ok(pebblesMood);
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

    [HttpGet("{patientId}/movementtimeweek")]
    public async Task<IActionResult> GetMovementTimeWeekAsync(Guid patientId)
    {
      var movementTimeWeek = await _patientService.GetMovementTimeWeekAsync(patientId);
      return Ok(movementTimeWeek);
    }

    [HttpGet("{patientId}/streakhistory")]
    public async Task<IActionResult> GetStreakHistoryAsync(Guid patientId)
    {
      var streakHistory = await _patientService.GetStreakHistoryAsync(patientId);
      return Ok(streakHistory);
    }

    [HttpGet("{patientId}/painhistory")]
    public async Task<IActionResult> GetPainHistoryAsync(Guid patientId)
    {
      var painHistory = await _patientService.GetPainHistoryAsync(patientId);
      return Ok(painHistory);
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
        // Handle exceptions
        return StatusCode(500, "Internal Server Error: " + ex.Message);
      }
    }

    [HttpGet("/checklink/{patientId}/specialist/{specialistId}")]
    public async Task<IActionResult> GetPatientIfLinkedToSpecialistAsync(Guid patientId, Guid specialistId)
    {
      var isLinked = await _patientService.IsPatientLinkedToSpecialistAsync(patientId, specialistId);

      if (!isLinked)
      {
        return Ok(new { Message = "Patient is not linked to the specialist" });
      }

      var patient = await _patientService.GetPatientByIdAsync(patientId);
      if (patient == null)
      {
        return NotFound();
      }
      return Ok(JsonConvert.SerializeObject(patient));
    }

  }
}


namespace Pebbles.Controllers.V2
{
  [ApiController]
  [ApiVersion("2.0")]
  [Route("patients")]
  [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller

  public class PatientController : ControllerBase
  {
    private readonly IPatientService _patientService;
    private readonly IQuestionnaireService _questionnaireService;

    public PatientController(
        IPatientService patientService,
        IQuestionnaireService questionnaireService
        )
    {
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
          return NotFound();
        }
        return Ok(JsonConvert.SerializeObject(patient));
      }
      catch (System.Exception)
      {
        return StatusCode(500, "Internal Server Error");
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
          return NotFound();
        }
        return Ok(JsonConvert.SerializeObject(patient));
      }
      catch (System.Exception)
      {
        return StatusCode(500, "Internal Server Error");
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
      catch (System.Exception)
      {
        return StatusCode(500, "Internal Server Error");
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
      catch (System.Exception)
      {
        return StatusCode(500, "Internal Server Error");
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
      catch (System.Exception)
      {
        return StatusCode(500, "Internal Server Error");
      }
    }

    [HttpGet("{patientId}/pebblesmood")]
    public async Task<IActionResult> GetPebblesMoodAsync(Guid patientId)
    {
      try
      {
        var pebblesMood = await _patientService.GetPebblesMoodAsync(patientId);
        return Ok(pebblesMood);
      }
      catch (System.Exception)
      {
        return StatusCode(500, "Internal Server Error");
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
      catch (System.Exception)
      {
        return StatusCode(500, "Internal Server Error");
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
      catch (System.Exception)
      {
        return StatusCode(500, "Internal Server Error");
      }
    }

    [HttpGet("{patientId}/movementtimeweek")]
    public async Task<IActionResult> GetMovementTimeWeekAsync(Guid patientId)
    {
      try
      {
        var movementTimeWeek = await _patientService.GetMovementTimeWeekAsync(patientId);
        return Ok(movementTimeWeek);
      }
      catch (System.Exception)
      {
        return StatusCode(500, "Internal Server Error");
      }
    }

    [HttpGet("{patientId}/streakhistory")]
    public async Task<IActionResult> GetStreakHistoryAsync(Guid patientId)
    {
      try
      {
        var streakHistory = await _patientService.GetStreakHistoryAsync(patientId);
        return Ok(streakHistory);
      }
      catch (System.Exception)
      {
        return StatusCode(500, "Internal Server Error");
      }
    }

    [HttpGet("{patientId}/painhistory")]
    public async Task<IActionResult> GetPainHistoryAsync(Guid patientId)
    {
      try
      {
        var painHistory = await _patientService.GetPainHistoryAsync(patientId);
        return Ok(painHistory);
      }
      catch (System.Exception)
      {
        return StatusCode(500, "Internal Server Error");
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
        // Handle exceptions
        return StatusCode(500, "Internal Server Error: " + ex.Message);
      }
    }
  }
}