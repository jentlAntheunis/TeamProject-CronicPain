using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Pebbles.Models;
using Pebbles.Services;
using Pebbles.Repositories;
using Pebbles.Context;

namespace Pebbles.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("question")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated patients can access this controller

    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly PebblesContext _context;

        public QuestionController(PebblesContext context, IQuestionService questionService)
        {
            _context = context;
            _questionService = questionService;
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

        [HttpPost("addquestionlist")]
        public async Task<IActionResult> AddQuestions([FromBody] List<Question> questions)
        {
            foreach (var question in questions)
            {
                question.Id = Guid.NewGuid();
            }

            var questionIds = await _questionService.AddQuestionsAsync(questions);

            return Ok(questionIds);
        }

    }
}

namespace Pebbles.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("question")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated patients can access this controller

    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionService _questionService;
        private readonly PebblesContext _context;

        public QuestionController(IQuestionRepository questionRepository, PebblesContext context, IQuestionService questionService)
        {
            _questionRepository = questionRepository;
            _context = context;
            _questionService = questionService;
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
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("addquestionlist")]
        public async Task<IActionResult> AddQuestions([FromBody] List<Question> questions)
        {
            try
            {
                foreach (var question in questions)
                {
                    question.Id = Guid.NewGuid();
                }

                var questionIds = await _questionService.AddQuestionsAsync(questions);

                return Ok(questionIds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}