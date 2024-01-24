using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

using Pebbles.Models;
using Pebbles.Services;

namespace Pebbles.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
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
            if (specialists == null)
            {
                return StatusCode(500);
            }
            var response = new
            {
                specialists = specialists
            };
            return Ok(JsonConvert.SerializeObject(response));
        }

        [HttpGet("{specialistId}")]
        public async Task<IActionResult> GetSpecialistByIdAsync(Guid specialistId)
        {
            var specialist = await _specialistService.GetSpecialistByIdAsync(specialistId);
            if (specialist == null)
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
            if (patients == null)
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
}

namespace Pebbles.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("specialists")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller

    public class SpecialistController : ControllerBase
    {
        private readonly ISpecialistService _specialistService;
        private readonly IPatientService _patientService;

        public SpecialistController(
            ISpecialistService specialistService,
            IPatientService patientService
            )
        {
            _specialistService = specialistService;
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpecialistsAsync()
        {
            try
            {
                var specialists = await _specialistService.GetAllSpecialistsAsync();
                if (specialists == null)
                {
                    return NotFound("Specialist not found");
                }
                var response = new
                {
                    specialists = specialists
                };
                return Ok(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{specialistId}")]
        public async Task<IActionResult> GetSpecialistByIdAsync(Guid specialistId)
        {
            try
            {
                var specialist = await _specialistService.GetSpecialistByIdAsync(specialistId);
                if (specialist == null)
                {
                    return NotFound("Specialist not found");
                }
                return Ok(JsonConvert.SerializeObject(specialist));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialistAsync([FromBody] Specialist specialist)
        {
            try
            {
                var newSpecialist = await _specialistService.CreateSpecialistAsync(specialist);
                return Ok(JsonConvert.SerializeObject(newSpecialist));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{specialistId}")]
        public async Task<IActionResult> UpdateSpecialistAsync(Guid specialistId, [FromBody] Specialist specialist)
        {
            try
            {
                specialist.Id = specialistId;
                var updatedSpecialist = await _specialistService.UpdateSpecialistAsync(specialist);
                return Ok(JsonConvert.SerializeObject(updatedSpecialist));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
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
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
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
                return StatusCode(500, "Internal Server Error");
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
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{specialistId}/patients")]
        public async Task<IActionResult> GetPatientsBySpecialistAsync(Guid specialistId)
        {
            try
            {
                var patients = await _patientService.GetPatientsBySpecialistAsync(specialistId);
                if (patients == null)
                {
                    return NotFound("Patients not found");
                }
                return Ok(JsonConvert.SerializeObject(patients));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("{specialistId}/patients/{patientId}/movementsuggestions")]
        public async Task<IActionResult> AddMovementSuggestionAsync(Guid specialistId, Guid patientId, [FromBody] MovementSuggestion movementSuggestion)
        {
            try
            {
                await _patientService.AddMovementSuggestion(specialistId, patientId, movementSuggestion);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}