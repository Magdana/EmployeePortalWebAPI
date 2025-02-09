using EmployeePortalWebAPI.DAL;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalWebAPI.Repositories.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmpolyeePortalDbContext _empolyeePortalDbContext;
    public EmployeeRepository(EmpolyeePortalDbContext empolyeePortalDbContext)
    {
        _empolyeePortalDbContext = empolyeePortalDbContext;
    }

    public async Task AddEmployee(EmployeeEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        await _empolyeePortalDbContext.Employees.AddAsync(entity);
        await SaveChangesAsync();
    }

    public async Task DeleteEmployee(Guid id)
    {
        var employee = await GetById(id);
        if (employee == null) throw new KeyNotFoundException($"Entity with ID {id} not found.");
        _empolyeePortalDbContext.Employees.Remove(employee);
        await SaveChangesAsync();
    }

    public async Task<List<EmployeeEntity>> GetAllAsync()
    {
        var employees = await _empolyeePortalDbContext.Employees.Where(e => e.IsDeleted == false).ToListAsync();
        return employees;
    }

    public async Task<EmployeeEntity> GetById(Guid id)
    {
        var employee = await _empolyeePortalDbContext.Employees.Where(e => e.IsDeleted == false).FirstOrDefaultAsync(e => e.Id == id);
        if (employee == null) throw new KeyNotFoundException($"Entity with ID {id} not found.");
        return employee;
    }

    public async Task<EmployeeEntity> UpdateEmployee(EmployeeEntity entity)
    {
        _empolyeePortalDbContext.Employees.Update(entity);
        await SaveChangesAsync();
        return entity;
    }
    public async Task<int> SaveChangesAsync()
    {
        var currentTime = DateTime.UtcNow.AddHours(4);

        foreach (var entry in _empolyeePortalDbContext.ChangeTracker.Entries().Where(e => e.Entity is EmployeeEntity))
        {
            var entityBase = (EmployeeEntity)entry.Entity;

            switch (entry.State)
            {
                case EntityState.Added:
                    entityBase.CreatedAt ??= currentTime;
                    break;
                case EntityState.Modified:
                    entityBase.UpdatedAt ??= currentTime;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entityBase.IsDeleted = true;
                    entityBase.DeletedAt = currentTime;
                    break;
            }
        }
        return await _empolyeePortalDbContext.SaveChangesAsync();
    }
}
