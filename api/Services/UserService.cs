using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
    Task<bool> CheckIfUserExistsAsync(string email);

    Task<User> LoginAsync(string email);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAvatarRepository _avatarRepository;
    private readonly IPatientRepository _patientRepository;

    public UserService(IConfiguration configuration)
    {
        _userRepository = new UserRepository(configuration);
        _avatarRepository = new AvatarRepository(configuration);
    }

    public async Task<List<User>> GetUsersAsync() => await _userRepository.GetUsersAsync();
    public async Task<User> GetUserByIdAsync(Guid id) => await _userRepository.GetUserByIdAsync(id);

    public async Task<User> UpdateUserAsync(User user) => await _userRepository.UpdateUserAsync(user);

    public async Task DeleteUserAsync(User user) => await _userRepository.DeleteUserAsync(user);

    public async Task<bool> CheckIfUserExistsAsync(string email)
    {
        var users = await GetUsersAsync();
        //return true or false: bool
        return users.Any(u => u.Email == email);
    }

    public async Task<User> LoginAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return user;
    }
}