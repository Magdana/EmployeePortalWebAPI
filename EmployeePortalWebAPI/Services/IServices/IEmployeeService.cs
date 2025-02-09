using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;

namespace EmployeePortalWebAPI.Services.IServices;

public interface IEmployeeService
{
    Task<List<EmployeeEntity>> GetAllAsync();
    Task<List<EmployeeEntity>> GetTopTenEarliestEmpolyessAsync();
    Task<List<EmployeeEntity>> GetTopHighSalaryEmployeesAsync();
    Task<EmployeeEntity> GetById(Guid id);
    Task<EmployeeEntity> UpdateEmployee(Guid Id, EmployeeDTOes.EmployeeUpdateDTO entity);
    Task DeleteEmployee(Guid id);
    Task<EmployeeEntity> AddEmployee(EmployeeDTOes.EmployeeAddDTO entity);
}
