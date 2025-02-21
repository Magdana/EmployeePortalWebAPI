using AutoMapper;
using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Repositories.IRepositories;
using EmployeePortalWebAPI.Repositories.Repositories;
using EmployeePortalWebAPI.Services.IServices;
using System.Security.Cryptography;
using System.Text;

namespace EmployeePortalWebAPI.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _employeeRepository = employeeRepository;

    }
    public async Task<UserEntity?> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            return null;
        return user;
    }

    public async Task<UserEntity> RegisterAsync(UserDTO.UserRegisterDTO request, UserRole role)
    {
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = role,
            EmployeeId = request.EmployeeEntityId
        };

        await _userRepository.AddUserAsync(user);
        return user;
    }



    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
    public async Task<List<UserDTO.UserInfoDTO>> GetAllUsersAsync()
    {
        var employeesIds = await _userRepository.GetAllEmployeesIdsAsync();
        var employess = new List<EmployeeEntity>();
        foreach (var employeeId in employeesIds)
        {
            var employee = await _employeeRepository.GetById(employeeId);
            if (employee != null)
            {
                employess.Add(employee);
            }

        }

        var usersInfoList = _mapper.Map<List<UserDTO.UserInfoDTO>>(employess);
        return usersInfoList;
    }
    public async Task<EmployeeDTOes.EmployeeDetailInfoDTO> GetUserDetailedInfoAsync(string id)
    {
        Guid.TryParse(id, out var userId);
        var employeeId = await _userRepository.GetEmployeeIdAsync(userId);
        var employee = _employeeRepository.GetById(employeeId).Result;
        var mappedEmployeeEntity = _mapper.Map<EmployeeDTOes.EmployeeDetailInfoDTO>(employee);
        return mappedEmployeeEntity;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _userRepository.DeleteUserAsync(id);
    }
  public async Task<UserDTO.UserEditGetDTO> EditUserAsync(string userName, UserDTO.UserEditDTO user)
    {
        var userToUpdate = await _userRepository.GetByUsernameAsync(userName);
        if (userToUpdate == null) throw new KeyNotFoundException($"Entity not found.");
        if (user == null) throw new ArgumentNullException();
        userToUpdate.Role = user?.Role ?? userToUpdate.Role;
        await _userRepository.EditUserAsync(userToUpdate);
        var mappedUser=_mapper.Map<UserDTO.UserEditGetDTO>(userToUpdate);
        return mappedUser;
    }

}

