using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface ITestService {
    Task<List<Test>> GetTestsAsync();
    Task<Test> GetTestAsync(int id);
    Task<Test> AddTestAsync(Test test);
    Task<Test> UpdateTestAsync(Test test);
    Task DeleteTestAsync(int id);
}

public class TestService : ITestService {
    private readonly ITestRepository _testRepository;

    public TestService(ITestRepository testRepository) {
        _testRepository = testRepository;
    }

    public async Task<List<Test>> GetTestsAsync() => await _testRepository.GetTestsAsync();
    public async Task<Test> GetTestAsync(int id) => await _testRepository.GetTestAsync(id);
    public async Task<Test> AddTestAsync(Test test) => await _testRepository.AddTestAsync(test);
    public async Task<Test> UpdateTestAsync(Test test) => await _testRepository.UpdateTestAsync(test);
    public async Task DeleteTestAsync(int id) => await _testRepository.DeleteTestAsync(id);
}