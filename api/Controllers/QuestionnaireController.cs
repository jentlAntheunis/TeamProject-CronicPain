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
[Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated patients can access this controller

public class QuestionnaireController : ControllerBase
{
  private readonly IConfiguration _configuration;
  private readonly IQuestionRepository _questionRepository;
  private readonly IQuestionService _questionService;

  private readonly IQuestionnaireRepository _questionnaireRepository;
  private readonly IQuestionnaireService _questionnaireService;


  public QuestionnaireController(IQuestionService questionService, IConfiguration configuration, IQuestionnaireRepository questionnaireRepository, IQuestionnaireService questionnaireService, IQuestionRepository questionRepository)
  {
    _configuration = configuration;
    _questionService = questionService;
    _questionnaireRepository = questionnaireRepository;
    _questionnaireService = questionnaireService;
    _questionRepository = questionRepository;
  }

  [HttpGet("movementquestionnaire/{patientId}")]
  public async Task<IActionResult> CreateMovementQuestionnaire(Guid patientId)
  {
    try
    {
      var questionnaireDTO = await _questionnaireRepository.AddMovementQuestionnaireAsync(patientId);

      if (questionnaireDTO == null)
      {
        return NotFound("Questionnaire not found.");
      }

      return Ok(questionnaireDTO);
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }
  }



  [HttpGet("bonusquestionnaire/{patientId}")]
  public async Task<IActionResult> CreateBonusQuestionnaire(Guid patientId)
  {
    try
    {
      var questionnaireDTO = await _questionnaireRepository.AddBonusQuestionnaireAsync(patientId);

      if (questionnaireDTO == null)
      {
        return NotFound("Questionnaire not found.");
      }

      return Ok(questionnaireDTO);
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }
  }

  [HttpGet("dailypainquestionnaire/{patientId}")]
  public async Task<IActionResult> CreateDailyPainQuestionnaire(Guid patientId)
  {
    try
    {
      var questionnaireDTO = await _questionnaireRepository.AddDailyPainQuestionnaireAsync(patientId);

      if (questionnaireDTO == null)
      {
        return NotFound("Questionnaire not found.");
      }

      return Ok(questionnaireDTO);
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }
  }


  [HttpGet("checkIfFirstQuestionnaireOfTheDay/{patientId}")]
  public async Task<IActionResult> CheckIfFirstQuestionnaireOfTheDay(Guid patientId)
  {
    try
    {
      var isFirstQuestionnaire = await _questionnaireService.CheckIfFirstQuestionnaireOfTheDay(patientId);
      return Ok(isFirstQuestionnaire);
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }
  }

  [HttpGet("checkifbonusdone/{patientId}")]
  public async Task<IActionResult> CheckIfBonusDone(Guid patientId)
  {
    try
    {
      var isBonusDone = await _questionnaireService.CheckIfBonusDone(patientId);
      return Ok(isBonusDone);
    }
    catch (Exception ex)
    {
      return StatusCode(500, "Internal server error.");
    }
  }
}
