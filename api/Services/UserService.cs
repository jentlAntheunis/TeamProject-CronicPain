using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> UpdateUserAsync(User user);
    Task<bool> CheckIfUserExistsAsync(string email);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IAvatarRepository _avatarRepository;
    private readonly ISpecialistRepository _specialistRepository;

    public UserService(IConfiguration configuration)
    {
        _userRepository = new UserRepository(configuration);
        _patientRepository = new PatientRepository(configuration);
        _avatarRepository = new AvatarRepository(configuration);
        _specialistRepository = new SpecialistRepository(configuration);
    }

    public async Task<List<User>> GetUsersAsync() => await _userRepository.GetUsersAsync();
    public async Task<User> GetUserByIdAsync(Guid id) => await _userRepository.GetUserByIdAsync(id);

    public async Task<User> UpdateUserAsync(User user) => await _userRepository.UpdateUserAsync(user);

    public async Task<bool> CheckIfUserExistsAsync(string email)
    {
        var users = await GetUsersAsync();
        //return true or false: bool
        return users.Any(u => u.Email == email);
    }
}