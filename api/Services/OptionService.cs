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
    private readonly IScaleRepository _scaleRepository;

    public OptionService(IOptionRepository optionRepository, IScaleRepository scaleRepository)
    {
        _optionRepository = optionRepository;
        _scaleRepository = scaleRepository;
    }

    public async Task<Option> GetOptionByIdAsync(Guid id) => await _optionRepository.GetOptionByIdAsync(id);

    public async Task<IEnumerable<Option>> GetAllOptionsAsync() => await _optionRepository.GetAllOptionsAsync();

    public async Task<Guid> CreateOptionAsync(Option option)
    {
        // Get the associated Scale based on ScaleId
        var scale = await _scaleRepository.GetScaleByIdAsync(option.ScaleId);

        if (scale != null)
        {
            if (scale.Name == "beweging")
            {
                switch (option.Position)
                {
                    case "1":
                        option.Content = "heel oneens";
                        break;
                    case "2":
                        option.Content = "oneens";
                        break;
                    case "3":
                        option.Content = "eens";
                        break;
                    case "4":
                        option.Content = "heel eens";
                        break;
                    
                }
            }
            else if (scale.Name == "bonus")
            {
                switch (option.Position)
                {
                    case "0":
                        option.Content = "helemaal niet";
                        break;
                    case "1":
                        option.Content = "in lichte mate";
                        break;
                    case "2":
                        option.Content = "in zekere mate";
                        break;
                    case "3":
                        option.Content = "in grote mate";
                        break;
                    case "4":
                        option.Content = "altijd";
                        break;
                    
                }
            }
        }
        

        return await _optionRepository.CreateOptionAsync(option);
    }

    public async Task<Option> UpdateOptionAsync(Option option) => await _optionRepository.UpdateOptionAsync(option);
}