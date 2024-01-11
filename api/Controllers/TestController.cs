using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using Pebbles.Models;
using Pebbles.Services;
using Pebbles.Repositories;

[ApiController]
[Route("tests")]
public class TestController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ITestService _testService;

    public TestController(IConfiguration configuration)
    {
        _configuration = configuration;
        _testService = new TestService(new TestRepository(_configuration));
    }

    [HttpGet]
    public async Task<IActionResult> GetTestsAsync()
    {
        var tests = await _testService.GetTestsAsync();
        if(tests == null)
        {
            return StatusCode(500);
        }
        return Ok(JsonConvert.SerializeObject(tests));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestAsync(int id)
    {
        var test = await _testService.GetTestAsync(id);
        if(test == null)
        {
            return NotFound();
        }
        return Ok(JsonConvert.SerializeObject(test));
    }

    [HttpPost]
    public async Task<IActionResult> AddTestAsync([FromBody] Test test)
    {
        var newTest = await _testService.AddTestAsync(test);
        return Ok(JsonConvert.SerializeObject(newTest));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTestAsync(int id, [FromBody] Test test)
    {
        test.Id = id;
        var updatedTest = await _testService.UpdateTestAsync(test);
        return Ok(JsonConvert.SerializeObject(updatedTest));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTestAsync(int id)
    {
        await _testService.DeleteTestAsync(id);
        return Ok();
    }
}