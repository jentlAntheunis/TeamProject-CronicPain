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
[Route("answers")]
[Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller

public class AnswerController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAnswerService _answerService;
    private readonly IOptionService _optionService;


    public AnswerController(IAnswerService answerService, IConfiguration configuration, IOptionService optionService)
    {
        _configuration = configuration;
        _answerService = answerService;
        _optionService = optionService;
    }

    [HttpPost]
    public async Task<IActionResult> SaveAnswers([FromBody] AnswerInputDTO answerInputDTO, [FromServices] IQuestionnaireService questionnaireService)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _answerService.ProcessAnswers(answerInputDTO.Answers, answerInputDTO.QuestionnaireId, answerInputDTO.QuestionnaireIndex, questionnaireService);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing your request to save answers: " + ex.Message);
        }
    }

    [HttpGet("user/{userId}/impacts")]
    public async Task<IActionResult> GetQuestionnaireImpactsByUserId(Guid userId)
    {
        try
        {
            var impacts = await _answerService.GetQuestionnaireImpactsByUserId(userId);
            return Ok(impacts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
        }
    }



}
