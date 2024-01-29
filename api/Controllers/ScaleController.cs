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

        var response = scales.Select(scale => new {
            id = scale.Id,
            name = scale.Name
        });

        return Ok(JsonConvert.SerializeObject(response));
      }
      catch (Exception ex)
      {
        return StatusCode(500, "Internal server error.");
      }
    }

}
