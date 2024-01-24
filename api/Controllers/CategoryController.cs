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


namespace Pebbles.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("category")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller


    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            if (categories == null)
            {
                return StatusCode(500);
            }

            var response = categories.Select(category => new
            {
                id = category.Id,
                name = category.Name
            });

            return Ok(JsonConvert.SerializeObject(response));
        }
    }
}

namespace Pebbles.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("category")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] // only authenticated users can access this controller


    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            if (categories == null)
            {
                return StatusCode(500);
            }

            var response = categories.Select(category => new
            {
                id = category.Id,
                name = category.Name
            });

            return Ok(JsonConvert.SerializeObject(response));
        }
    }
}
