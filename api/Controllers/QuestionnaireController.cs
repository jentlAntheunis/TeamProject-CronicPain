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
[Route("questionnaires")]
[Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller

public class QuestionController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService, IConfiguration configuration)
    {
        _configuration = configuration;
        _questionService = questionService;
    }

    [HttpGet("bewegingsvragen")]
    public async Task<IActionResult> GetBewegingsvragen(Guid userId)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            // Handle exceptions and return an error response if necessary
            return StatusCode(500, "Internal Server Error");
        }
    }

    

}