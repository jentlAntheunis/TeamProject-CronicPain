using Pebbles.Repositories;
using Pebbles.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pebbles.Services;

public interface IStoreService
{
    Task<List<Color>> GetPatientStoreAsync(Guid patientId);
    Task PurchaseColorAsync(Guid patientId, Guid colorId);

}

public class StoreService : IStoreService
{
    private readonly IColorRepository _colorRepository;
    private readonly IPatientRepository _patientRepository;

    public StoreService(
        IColorRepository colorRepository,
        IPatientRepository patientRepository
        )
    {
        _colorRepository = colorRepository;
        _patientRepository = patientRepository;
    }

    public async Task<List<Color>> GetPatientStoreAsync(Guid patientId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if(patient == null)
        {
            throw new Exception("Patient not found");
        }
        var colors = await _colorRepository.GetAllColorsAsync();
        foreach(var color in colors)
        {
            if(patient.Colors.Contains(color))
            {
                color.Owned = true;
            }
        }
        return colors;
    }

    public async Task PurchaseColorAsync(Guid patientId, Guid colorId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        var color = await _colorRepository.GetColorByIdAsync(colorId);
        if(patient == null || color == null)
        {
            throw new Exception("Patient or color not found");
        }
        if(patient.Coins < color.Price)
        {
            throw new Exception("Patient does not have enough coins");
        }
        patient.Coins -= color.Price;
        patient.Colors.Add(color);
        await _patientRepository.UpdatePatientAsync(patient);
    }
}