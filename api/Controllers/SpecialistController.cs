using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using Pebbles.Models;
using Pebbles.Services;
using Pebbles.Repositories;
using Pebbles.Context;

[ApiController]
[Route("specialists")]
public class SpecialistController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly PatientService _patientService;
    private readonly SpecialistService _specialistService;

    public SpecialistController(IConfiguration configuration)
    {
        _configuration = configuration;
        PebblesContext context = new PebblesContext(_configuration);
        _patientService = new PatientService(new PatientRepository(context), new SpecialistRepository(context));
    }

    [HttpPost("{specialistId}/patients")]
    public async Task<ActionResult<Patient>> AddPatientBySpecialistAsync(Guid specialistId, [FromBody] Patient patient)
    {
        try
        {
            var newPatient = await _patientService.AddPatientBySpecialistAsync(specialistId, patient);
            return Created(newPatient.Id.ToString(), newPatient);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500);
        }
    }
}