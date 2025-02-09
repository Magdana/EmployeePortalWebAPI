using EmployeePortalWebAPI.Entities;

namespace EmployeePortalWebAPI.Repositories.IRepositories;

public interface IEmployeeRepository
{
    Task<List<EmployeeEntity>> GetAllAsync();
    Task<EmployeeEntity> GetById(Guid id);
    Task<EmployeeEntity> UpdateEmployee(EmployeeEntity entity);
    Task DeleteEmployee(Guid id);
    Task AddEmployee(EmployeeEntity entity);
    Task<int> SaveChangesAsync();
}
