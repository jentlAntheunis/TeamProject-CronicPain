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
using Google.Rpc;

namespace Pebbles.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("store")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller

    public class StoreController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IStoreService _storeService;

        public StoreController(
            IStoreService storeService,
            IConfiguration configuration
            )
        {
            _configuration = configuration;
            _storeService = storeService;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetPatientStoreAsync(Guid patientId)
        {
            var store = await _storeService.GetPatientStoreAsync(patientId);
            return Ok(JsonConvert.SerializeObject(store));
        }

        [HttpPut("{patientId}/buy/{colorId}")]
        public async Task<IActionResult> BuyColorAsync(Guid patientId, Guid colorId)
        {
            await _storeService.PurchaseColorAsync(patientId, colorId);
            return Ok();
        }

        [HttpPut("{patientId}/use/{colorId}")]
        public async Task<IActionResult> UseColorAsync(Guid patientId, Guid colorId)
        {
            await _storeService.UseColorAsync(patientId, colorId);
            return Ok();
        }
    }
}

namespace Pebbles.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("store")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller

    public class StoreController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IStoreService _storeService;

        public StoreController(
            IStoreService storeService,
            IConfiguration configuration
            )
        {
            _configuration = configuration;
            _storeService = storeService;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetPatientStoreAsync(Guid patientId)
        {
            try
            {
                var store = await _storeService.GetPatientStoreAsync(patientId);
                return Ok(JsonConvert.SerializeObject(store));
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{patientId}/buy/{colorId}")]
        public async Task<IActionResult> BuyColorAsync(Guid patientId, Guid colorId)
        {
            try
            {
                await _storeService.PurchaseColorAsync(patientId, colorId);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{patientId}/use/{colorId}")]
        public async Task<IActionResult> UseColorAsync(Guid patientId, Guid colorId)
        {
            try
            {
                await _storeService.UseColorAsync(patientId, colorId);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}