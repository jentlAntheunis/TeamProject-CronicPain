using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using Pebbles.Services;

namespace Pebbles.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("tests")]
    public class TestController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public TestController(IPatientService patientService)
        {
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
        public TestController() { }

        [HttpGet("version")]
        public IActionResult GetVersion()
        {
            return Ok("Version 2.0");
        }
    }
}