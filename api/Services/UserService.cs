using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserAsync(Guid id);
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid id);

    Task CheckIfUserExistsAsync(Guid id);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetUsersAsync() => await _userRepository.GetUsersAsync();
    public async Task<User> GetUserAsync(Guid id) => await _userRepository.GetUserAsync(id);
    public async Task<User> AddUserAsync(User user) => await _userRepository.AddUserAsync(user);
    public async Task<User> UpdateUserAsync(User user) => await _userRepository.UpdateUserAsync(user);
    public async Task DeleteUserAsync(Guid id) => await _userRepository.DeleteUserAsync(id);

    public async Task<bool> CheckIfUserExistsAsync(Guid id)
    {
        var user = await GetUserAsync(id);
        //return true or false: bool
        return user != null;
    }

}