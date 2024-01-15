using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

using Pebbles.Models;
using Pebbles.Services;
using Pebbles.Repositories;

[ApiController]
[Route("specialists")]

public class SpecialistController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ISpecialistService _specialistService;
    private readonly IPatientService _patientService;

    public SpecialistController(IConfiguration configuration)
    {
        _configuration = configuration;
        _specialistService = new SpecialistService(_configuration);
        _patientService = new PatientService(_configuration);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSpecialistsAsync()
    {
        var specialists = await _specialistService.GetAllSpecialistsAsync();
        if(specialists == null)
        {
            return StatusCode(500);
        }
        return Ok(JsonConvert.SerializeObject(specialists));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSpecialistByIdAsync(Guid id)
    {
        var specialist = await _specialistService.GetSpecialistByIdAsync(id);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSpecialistAsync(Guid id, [FromBody] Specialist specialist)
    {
        specialist.Id = id;
        var updatedSpecialist = await _specialistService.UpdateSpecialistAsync(specialist);
        return Ok(JsonConvert.SerializeObject(updatedSpecialist));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpecialistAsync(Guid id)
    {
        await _specialistService.DeleteSpecialistAsync(id);
        return Ok();
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
}