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
[Route("specialists")]
[Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller

public class SpecialistController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ISpecialistService _specialistService;
    private readonly IPatientService _patientService;

    public SpecialistController(
        ISpecialistService specialistService, 
        IPatientService patientService, 
        IConfiguration configuration
        )
    {
        _configuration = configuration;
        _specialistService = specialistService;
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSpecialistsAsync()
    {
        var specialists = await _specialistService.GetAllSpecialistsAsync();
        if(specialists == null)
        {
            return StatusCode(500);
        }
        var response = new {
            specialists = specialists
        };
        return Ok(JsonConvert.SerializeObject(response));
    }

    [HttpGet("{specialistId}")]
    public async Task<IActionResult> GetSpecialistByIdAsync(Guid specialistId)
    {
        var specialist = await _specialistService.GetSpecialistByIdAsync(specialistId);
        if(specialist == null)
        {
            return NotFound();
        }
        return Ok(JsonConvert.SerializeObject(specialist));
    }

    [HttpPost]
    public async Task<IActionResult> CreateSpecialistAsync([FromBody] Specialist specialist)
    {
        var newSpecialist = await _specialistService.CreateSpecialistAsync(specialist);
        return Ok(JsonConvert.SerializeObject(newSpecialist));
    }

    [HttpPut("{specialistId}")]
    public async Task<IActionResult> UpdateSpecialistAsync(Guid specialistId, [FromBody] Specialist specialist)
    {
        specialist.Id = specialistId;
        var updatedSpecialist = await _specialistService.UpdateSpecialistAsync(specialist);
        return Ok(JsonConvert.SerializeObject(updatedSpecialist));
    }

    [HttpPost("send-email/{specialistId}")]
    public IActionResult SendEmail(Guid specialistId, [FromBody] AnonymousPatientData patientData)
    {
        try
        {
            var specialist = _specialistService.GetSpecialistByIdAsync(specialistId).Result;

            if (specialist == null)
            {
                return NotFound("Specialist not found");
            }

            _specialistService.SendEmailWithInvitation(specialist.FirstName, specialist.LastName, patientData.FirstName, patientData.LastName, patientData.Email);

            return Ok("Invitation email sent successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error sending email: {ex.Message}");
        }
    }

    [HttpPost("{specialistId}/patients")]
    public async Task<ActionResult<Patient>> AddPatientBySpecialistAsync(Guid specialistId, [FromBody] Patient patient)
    {
        try
        {
            var newPatientId = await _patientService.AddPatientBySpecialistAsync(specialistId, patient);
            return Created("Ok", newPatientId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500);
        }
    }

    [HttpPost("{specialistId}/patients/addlist")]
    public async Task<IActionResult> AddPatientListBySpecialistAsync(Guid specialistId, [FromBody] List<Patient> patients)
    {
        try
        {
            await _patientService.AddPatientListToSpecialistAsync(specialistId, patients);
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500);
        }
    }

    [HttpGet("{specialistId}/patients")]
    public async Task<IActionResult> GetPatientsBySpecialistAsync(Guid specialistId)
    {
        var patients = await _patientService.GetPatientsBySpecialistAsync(specialistId);
        if(patients == null)
        {
            return StatusCode(500);
        }
        return Ok(JsonConvert.SerializeObject(patients));
    }

    [HttpPost("{specialistId}/patients/{patientId}/movementsuggestions")]
    public async Task<IActionResult> AddMovementSuggestionAsync(Guid specialistId, Guid patientId, [FromBody] MovementSuggestion movementSuggestion)
    {
        await _patientService.AddMovementSuggestion(specialistId, patientId, movementSuggestion);
        return Ok();
    }
}