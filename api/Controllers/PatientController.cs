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
}