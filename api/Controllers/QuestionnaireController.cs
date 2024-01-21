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
    private readonly IQuestionnaireService _questionnaireService;


    public QuestionController(IQuestionService questionService, IConfiguration configuration, IQuestionnaireRepository questionnaireRepository)
    {
        _configuration = configuration;
        _questionService = questionService;
        _questionnaireRepository = questionnaireRepository;
    }

    [HttpGet("movementquestionnaire/{userId}")]
    public async Task<IActionResult> CreateMovementQuestionnaire(Guid userId)
    {
        try
        {
            var questionnaireDTO = await _questionnaireRepository.AddMovementQuestionnaireAsync(userId);

            if (questionnaireDTO == null)
            {
                return NotFound("Questionnaire not found.");
            }

            return Ok(questionnaireDTO);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create movement questionnaire: {ex.Message}");
        }
    }

    

    [HttpGet("bonusquestionnaire/{userId}")]
    public async Task<IActionResult> CreateBonusQuestionnaire(Guid userId)
    {
        try
        {
            var questionnaireDTO = await _questionnaireRepository.AddBonusQuestionnaireAsync(userId);

            if (questionnaireDTO == null)
            {
                return NotFound("Questionnaire not found.");
            }

            return Ok(questionnaireDTO);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create bonus questionnaire: {ex.Message}");
        }
    }

    [HttpGet("dailypainquestionnaire/{userId}")]
    public async Task<IActionResult> CreateDailyPainQuestionnaire(Guid userId)
    {
        try
        {
            var questionnaireDTO = await _questionnaireRepository.AddDailyPainQuestionnaireAsync(userId);

            if (questionnaireDTO == null)
            {
                return NotFound("Questionnaire not found.");
            }

            return Ok(questionnaireDTO);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create daily pain questionnaire: {ex.Message}");
        }
    }

    [HttpGet("isfirstquestionnaireoftheday/{userId}")]
    public async Task<IActionResult> IsFirstQuestionnaireOfTheDay(Guid userId)
    {
        try
        {
            var isFirstQuestionnaire = await _questionnaireService.CheckIfFirstQuestionnaireOfTheDay(userId);

            return Ok(isFirstQuestionnaire);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to check if it's the first questionnaire of the day: {ex.Message}");
        }
    }




    

}