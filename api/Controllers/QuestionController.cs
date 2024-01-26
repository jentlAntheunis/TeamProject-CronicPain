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
    private readonly ICategoryRepository _categoryRepository;

    public QuestionController(IQuestionRepository questionRepository, PebblesContext context, ICategoryRepository categoryRepository)
    {
        _questionRepository = questionRepository;
        _context = context;
        _categoryRepository = categoryRepository;
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

    [HttpGet("getallquestions")]
    public async Task<IActionResult> GetAllQuestions()
    {
        try
        {
            var questions = await _questionRepository.GetAllQuestionsAsync();

            if (questions == null)
            {
                return NotFound("No questions found.");
            }

            var questionDtos = new List<QuestionDTO>();

            foreach (var question in questions)
            {
                var questionDto = new QuestionDTO
                {
                    Id = question.Id,
                    CategoryId = question.CategoryId,
                    SpecialistId = question.SpecialistId,
                    ScaleId = question.ScaleId,
                    Content = question.Content
                };

                // Retrieve the category name based on CategoryId
                if (question.CategoryId != null) // Check if CategoryId is not null
                {
                    var categoryName = await _categoryRepository.GetCategoryNameByIdAsync(question.CategoryId);
                    if (categoryName != null)
                    {
                        questionDto.CategoryName = categoryName;
                    }
                }

                questionDtos.Add(questionDto);
            }

            return Ok(questionDtos);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get questions: {ex.Message}");
        }
    }



}
