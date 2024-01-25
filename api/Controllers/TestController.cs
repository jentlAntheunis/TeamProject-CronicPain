using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

using Pebbles.Models;
using Pebbles.Services;
using Pebbles.Repositories;

[ApiController]
[Route("tests")]
public class TestController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IPatientService _patientService;

    public TestController(
        IConfiguration configuration, 
        IUserService userService,
        IPatientService patientService
        )
    {
        _userService = userService;
        _patientService = patientService;
    }

    [HttpGet("patientdetails/{patientId}")]
    public async Task<IActionResult> GetPatientDetailsByIdAsync(Guid patientId)
    {
        var patient = await _patientService.GetPatientDetailsByIdAsync(patientId);
        return Ok(JsonConvert.SerializeObject(patient));
    }
}