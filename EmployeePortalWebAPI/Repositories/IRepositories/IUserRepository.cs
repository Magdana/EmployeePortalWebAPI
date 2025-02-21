using EmployeePortalWebAPI.Entities;

namespace EmployeePortalWebAPI.Repositories.IRepositories;

public interface IUserRepository
{
    Task<UserEntity?> GetByUsernameAsync(string username);
    Task AddUserAsync(UserEntity user);
    Task<List<Guid>> GetAllEmployeesIdsAsync();
    Task<Guid> GetEmployeeIdAsync(Guid Id);
    Task DeleteUserAsync(Guid id);
    Task<UserEntity> EditUserAsync(UserEntity user);
}
