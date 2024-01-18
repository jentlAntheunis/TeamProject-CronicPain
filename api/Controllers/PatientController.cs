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

    [HttpGet("{patientId}/movementsession/start")]
    public async Task<IActionResult> StartMovementSessionAsync(Guid patientId)
    {
        var movementSessionId = await _patientService.StartMovementSessionAsync(patientId);
        return Ok(movementSessionId);
    }

    [HttpGet("{movementSessionId}/movementsession/end")]
    public async Task<IActionResult> EndMovementSessionAsync(Guid movementSessionId)
    {
        await _patientService.EndMovementSessionAsync(movementSessionId);
        return Ok();
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

    [HttpGet("{patientId}/addcoins/{amount}")]
    public async Task<IActionResult> AddCoinsAsync(Guid patientId, int amount)
    {
        await _patientService.AddCoinsAsync(patientId, amount);
        return Ok();
    }
}