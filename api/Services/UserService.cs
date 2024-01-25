using Newtonsoft.Json;
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
  private readonly ISpecialistRepository _specialistRepository;
  private readonly ILoginRepository _loginRepository;

  public UserService(
      IUserRepository userRepository,
      IAvatarRepository avatarRepository,
      IPatientRepository patientRepository,
      ILoginRepository loginRepository,
      ISpecialistRepository specialistRepository
      )
  {
    _userRepository = userRepository;
    _avatarRepository = avatarRepository;
    _patientRepository = patientRepository;
    _loginRepository = loginRepository;
    _specialistRepository = specialistRepository;
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
    //check if user is a patient
    var patient = await _patientRepository.GetPatientByIdAsync(user.Id);
    if (patient != null)
    {
      user = patient;
    }
    //check if user is a specialist
    var specialist = await _specialistRepository.GetSpecialistByIdAsync(user.Id);
    if (specialist != null)
    {
      user = specialist;
    }
    await _loginRepository.CreateLoginByUserIdAsync(user.Id);
    return user;
  }
}
