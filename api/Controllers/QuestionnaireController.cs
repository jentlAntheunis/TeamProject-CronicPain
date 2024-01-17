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
            // Create a "bewegingsvragen" questionnaire for the user
            var bewegingsvragenQuestionnaire = await _questionnaireService.AddQuestionnaireAsync(userId, "bewegingsvragen");

            if (bewegingsvragenQuestionnaire != null)
            {
                //Retrieve the questionnaire's questions
                var questions = await _questionService.GetQuestionsByQuestionnaireIdAsync(bewegingsvragenQuestionnaire.Id);

                //return the questions and their linked scale and options

            }
            else
            {
                return BadRequest("Failed to create a 'bewegingsvragen' questionnaire.");
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and return an error response if necessary
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("bonusvragen")]
    public async Task<IActionResult> GetBonusvragen(Guid userId)
    {
        try
        {
            //create a "bonusvragen" questionnaire for the user
            var bonusvragenQuestionnaire = await _questionnaireService.AddQuestionnaireAsync(userId, "bonusvragen");

            if (bonusvragenQuestionnaire != null)
            {
                //Retrieve the questionnaire's questions
                var questions = await _questionService.GetQuestionsByQuestionnaireIdAsync(bonusvragenQuestionnaire.Id);

                //return the questions and their linked scale and options

            }
            else
            {
                return BadRequest("Failed to create a 'bonusvragen' questionnaire.");
            }GetBewegingsvragen
        }
        catch (Exception ex)
        {
            // Handle exceptions and return an error response if necessary
            return StatusCode(500, "Internal Server Error");
        }
    }

    

}