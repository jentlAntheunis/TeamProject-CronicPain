using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

using Pebbles.Repositories;

namespace Pebbles.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("scale")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller


    public class ScaleController : ControllerBase
    {
        private readonly IScaleRepository _scaleRepository;

        public ScaleController(IScaleRepository scaleRepository)
        {
            _scaleRepository = scaleRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllScalesAsync()
        {
            var scales = await _scaleRepository.GetAllScalesAsync();
            if (scales == null)
            {
                return StatusCode(500);
            }

            var response = scales.Select(scale => new
            {
                id = scale.Id,
                name = scale.Name
            });

            return Ok(JsonConvert.SerializeObject(response));
        }

    }
}

namespace Pebbles.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("scale")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller
    public class ScaleController : ControllerBase
    {
        private readonly IScaleRepository _scaleRepository;

        public ScaleController(IScaleRepository scaleRepository)
        {
            _scaleRepository = scaleRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllScalesAsync()
        {
            try
            {
                var scales = await _scaleRepository.GetAllScalesAsync();
                if (scales == null)
                {
                    return StatusCode(500);
                }

                var response = scales.Select(scale => new
                {
                    id = scale.Id,
                    name = scale.Name
                });

                return Ok(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}