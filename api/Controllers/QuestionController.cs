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
using Pebbles.Context;


[ApiController]
[Route("question")]
[Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated patients can access this controller

public class QuestionController : ControllerBase
{
    private readonly IQuestionRepository _questionRepository;
    private readonly PebblesContext _context;

    public QuestionController(IQuestionRepository questionRepository, PebblesContext context)
    {
        _questionRepository = questionRepository;
        _context = context;
    }

    [HttpPost("addquestion")]
    public async Task<IActionResult> AddQuestion([FromBody] Question newQuestion)
    {
        try
        {
            var newQuestionId = Guid.NewGuid();

            if (string.IsNullOrEmpty(newQuestion.Content) || newQuestion.CategoryId == Guid.Empty || newQuestion.SpecialistId == Guid.Empty || newQuestion.ScaleId == Guid.Empty)
            {
                return BadRequest("Invalid data. Please provide valid values for all properties.");
            }

            newQuestion.Id = newQuestionId;

            _context.Question.Add(newQuestion);
            await _context.SaveChangesAsync();

            return Ok("Question added successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to add question: {ex.Message}");
        }
    }

}