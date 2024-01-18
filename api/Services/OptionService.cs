using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IOptionService
{
    Task<Option> GetOptionByIdAsync(Guid id);
    Task<IEnumerable<Option>> GetAllOptionsAsync();
    Task<Guid> CreateOptionAsync(Option option);
    Task<Option> UpdateOptionAsync(Option option);
}


public class OptionService : IOptionService
{
    private readonly IOptionRepository _optionRepository;

    public OptionService(IOptionRepository optionRepository)
    {
        _optionRepository = optionRepository;
    }

    public async Task<Option> GetOptionByIdAsync(Guid id) => await _optionRepository.GetOptionByIdAsync(id);

    public async Task<IEnumerable<Option>> GetAllOptionsAsync() => await _optionRepository.GetAllOptionsAsync();

    public async Task<Guid> CreateOptionAsync(Option option)
    {
        if (option.ScaleName == "oneens_eens")
        {
            option.Content = "xxx";
            option.Position = 1;
        }
        else if (option.ScaleName == "niet_altijd")
        {
            // Customize the Content and Position for niet_altijd
            option.Content = "xxx";
            option.Position = 2;
        }

        return await _optionRepository.CreateOptionAsync(option);
    }

    public async Task<Option> UpdateOptionAsync(Option option) => await _optionRepository.UpdateOptionAsync(option);
}