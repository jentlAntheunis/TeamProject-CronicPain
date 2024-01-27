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
            return StatusCode(500, $"Internal server error.");
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

                if (question.CategoryId != null)
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
            return StatusCode(500, $"Internal server error.");
        }
    }

    [HttpPut("updatequestion/{id}")]
    public async Task<IActionResult> UpdateQuestion(Guid id, [FromBody] Question updatedQuestion)
    {
        try
        {
            var existingQuestion = await _context.Question.FindAsync(id);
            if (existingQuestion == null)
            {
                return NotFound($"Question with ID {id} not found.");
            }

            existingQuestion.CategoryId = updatedQuestion.CategoryId;
            existingQuestion.SpecialistId = updatedQuestion.SpecialistId;
            existingQuestion.ScaleId = updatedQuestion.ScaleId;
            existingQuestion.Content = updatedQuestion.Content;

            _context.Question.Update(existingQuestion);
            await _context.SaveChangesAsync();

            return Ok($"Question with ID {id} updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error.");
        }
    }



    [HttpPost("addquestions")]
    public async Task<IActionResult> AddQuestions([FromBody] QuestionListDTO questionList)
    {
      try
      {
        if (questionList?.Data == null || !questionList.Data.Any())
        {
            return BadRequest("Invalid data. Please provide a list of questions.");
        }

        foreach (var QuestionDTO in questionList.Data)
        {
            var newQuestion = new Question
            {
                Id = Guid.NewGuid(),
                Content = QuestionDTO.Content
            };

            if (QuestionDTO.CategoryId != null)
                newQuestion.CategoryId = (Guid)QuestionDTO.CategoryId;

            if (QuestionDTO.SpecialistId != null)
                newQuestion.SpecialistId = (Guid)QuestionDTO.SpecialistId;

            if (QuestionDTO.ScaleId != null)
                newQuestion.ScaleId = (Guid)QuestionDTO.ScaleId;

            _context.Question.Add(newQuestion);
        }


        await _context.SaveChangesAsync();

        return Ok("Questions added successfully.");
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error.");
      }
    }


}
