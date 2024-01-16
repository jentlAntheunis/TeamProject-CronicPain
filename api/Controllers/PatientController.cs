using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

using Pebbles.Models;
using Pebbles.Services;
using Pebbles.Repositories;

[ApiController]
[Route("patients")]

public class PatientController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IPatientService _patientService;

    public PatientController(IConfiguration configuration, IPatientService patientService)
    {
        _configuration = configuration;
        _patientService = patientService;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatientAsync(Guid id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);
        if(patient == null)
        {
            return NotFound();
        }
        await _patientService.DeletePatientAsync(patient);
        return Ok();
    }
}