using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid id);
    Task<bool> CheckIfUserExistsAsync(string email);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPatientSpecialistRepository _patientSpecialistRepository;

    public UserService(IConfiguration configuration)
    {
        _userRepository = new UserRepository(configuration);
        _patientSpecialistRepository = new PatientSpecialistRepository(configuration);
    }

    public async Task<List<User>> GetUsersAsync() => await _userRepository.GetUsersAsync();
    public async Task<User> GetUserByIdAsync(Guid id) => await _userRepository.GetUserByIdAsync(id);

    public async Task<User> AddUserAsync(User user) => await _userRepository.AddUserAsync(user);
    public async Task<User> UpdateUserAsync(User user) => await _userRepository.UpdateUserAsync(user);
    public async Task DeleteUserAsync(Guid id) {
        //find user references in PatientSpecialist table
        var patientSpecialists = await _patientSpecialistRepository.GetPatientSpecialistsByIdAsync(id);
        //delete user references in PatientSpecialist table
        foreach(var ps in patientSpecialists)
        {
            await _patientSpecialistRepository.DeletePatientSpecialistAsync(ps);
        }

        await _userRepository.DeleteUserAsync(id);
    }

    public async Task<bool> CheckIfUserExistsAsync(string email)
    {
        var users = await GetUsersAsync();
        //return true or false: bool
        return users.Any(u => u.Email == email);
    }

}