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
    public async Task DeleteUserAsync(Guid id)
    {
        var user = _empolyeePortalDbContext.Users.Where(e => e.Id == id).FirstOrDefault();
        if (user == null) throw new KeyNotFoundException("user not found!");
        _empolyeePortalDbContext.Users.Remove(user);
        await _empolyeePortalDbContext.SaveChangesAsync();
    }
    public async Task<UserEntity> EditUserAsync(UserEntity user)
    {
        _empolyeePortalDbContext.Users.Update(user);
        await _empolyeePortalDbContext.SaveChangesAsync();
        return user;
    }
}
