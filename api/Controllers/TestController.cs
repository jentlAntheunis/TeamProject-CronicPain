using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

using Pebbles.Models;
using Pebbles.Services;
using Pebbles.Repositories;

namespace Pebbles.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
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

        [HttpGet("version")]
        public IActionResult GetVersion()
        {
            return Ok("Version 1.0");
        }
    }
}

namespace Pebbles.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
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

        [HttpGet("version")]
        public IActionResult GetVersion()
        {
            return Ok("Version 2.0");
        }
    }
}