using EmployeePortalWebAPI.DAL;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalWebAPI.Repositories.Repositories;

public class UserRepository : IUserRepository
{
    private readonly EmpolyeePortalDbContext _empolyeePortalDbContext;
    public UserRepository(EmpolyeePortalDbContext empolyeePortalDbContext)
    {
        _empolyeePortalDbContext = empolyeePortalDbContext;
    }
    public async Task<UserEntity?> GetByUsernameAsync(string username) =>
        await _empolyeePortalDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

    public async Task AddUserAsync(UserEntity user)
    {
        _empolyeePortalDbContext.Users.Add(user);
        await _empolyeePortalDbContext.SaveChangesAsync();
    }
    public async Task<List<Guid>> GetAllEmployeesIdsAsync()
    {
        var employeeIds = await _empolyeePortalDbContext.Users.Where(u => u.EmployeeId != null).Select(u => u.EmployeeId.Value).ToListAsync();
        return employeeIds;

    }
    public async Task<Guid> GetEmployeeIdAsync(Guid id)
    {
        var user = await _empolyeePortalDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) throw new KeyNotFoundException($"Entity with ID {id} not found.");
        if (!user.EmployeeId.HasValue)
            throw new InvalidOperationException($"User with ID {id} does not have an associated Employee.");
        var employeeId = user.EmployeeId.Value;
        return employeeId;
    }
}
