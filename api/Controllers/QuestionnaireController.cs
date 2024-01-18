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
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuestionService _questionService;

    private readonly IQuestionnaireRepository _questionnaireRepository;


    public QuestionController(IQuestionService questionService, IConfiguration configuration)
    {
        _configuration = configuration;
        _questionService = questionService;
    }

    [HttpGet("movementquestionnaire")]
    public async Task<IActionResult> CreateMovementQuestionnaire(Guid userId)
    {
        try
        {
            var questionnaire = await _questionnaireRepository.AddMovementQuestionnaireAsync(userId);

            return Ok(questionnaire);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create movement questionnaire: {ex.Message}");
        }
    }

    [HttpGet("bonusquestionnaire")]
    public async Task<IActionResult> CreateBonusQuestionnaire(Guid userId)
    {
        try
        {
            var questionnaire = await _questionnaireRepository.AddBonusQuestionnaireAsync(userId);
            return Ok(questionnaire);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create bonus questionnaire: {ex.Message}");
        }
    }    

    

}