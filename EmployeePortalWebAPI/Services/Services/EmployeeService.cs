using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Repositories.IRepositories;
using EmployeePortalWebAPI.Services.IServices;

namespace EmployeePortalWebAPI.Services.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public async Task<EmployeeEntity> AddEmployee(EmployeeDTOes.EmployeeAddDTO entity)
    {
        var employee = new EmployeeEntity
        {
            Name = entity.Name,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            Salary = entity.Salary,
            CompanyId=entity.CompanyId
        };
        await _employeeRepository.AddEmployee(employee);
        return await Task.FromResult(employee);
    }

    public async Task DeleteEmployee(Guid id)
    {
        await _employeeRepository.DeleteEmployee(id);
    }

    public async Task<List<EmployeeEntity>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees;
    }

    public async Task<List<EmployeeEntity>> GetTopTenEarliestEmpolyessAsync()
    {
        var employees = await _employeeRepository.GetTopTenEarliestEmpolyessAsync();
        return employees;
    }

    public async Task<List<EmployeeEntity>> GetTopHighSalaryEmployeesAsync()
    {
        var employees = await _employeeRepository.GetTopHighSalaryEmployeesAsync();
        return employees;
    }

    public async Task<List<EmployeeEntity>> GetSoftDeletedEmployeesAsync()
    {
        var employees = await _employeeRepository.GetSoftDeletedEmployeesAsync();
        return employees;
    }

    public async Task<EmployeeEntity> GetById(Guid id)
    {
        var employee = await _employeeRepository.GetById(id);
        return employee;
    }

    public async Task<EmployeeEntity> UpdateEmployee(Guid id, EmployeeDTOes.EmployeeUpdateDTO entity)
    {
        var employee = await _employeeRepository.GetById(id);
        if (employee == null) throw new KeyNotFoundException($"Entity with ID {id} not found.");
        if (entity == null) throw new ArgumentNullException();
        employee.Name = entity?.Name ?? employee.Name;
        employee.Email=entity?.Email ?? employee.Email;
        employee.PhoneNumber = entity?.PhoneNumber ?? employee.PhoneNumber;
        employee.Salary = entity?.Salary ?? employee.Salary;
        employee.CompanyId=entity?.CompanyId ?? employee.CompanyId;
        await _employeeRepository.UpdateEmployee(employee);
        return employee;
    }
}
