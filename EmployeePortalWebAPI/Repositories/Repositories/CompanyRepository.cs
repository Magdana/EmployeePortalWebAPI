using EmployeePortalWebAPI.DAL;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EmployeePortalWebAPI.Repositories.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly EmpolyeePortalDbContext _empolyeePortalDbContext;
    public CompanyRepository(EmpolyeePortalDbContext empolyeePortalDbContext)
    {
        _empolyeePortalDbContext = empolyeePortalDbContext;
    }

    public async Task AddCompany(CompanyEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        await _empolyeePortalDbContext.Companies.AddAsync(entity);
        await SaveChangesAsync();
    }


    public async Task<CompanyEntity> GetById(Guid id)
    {
        var company = await _empolyeePortalDbContext.Companies.Where(e => e.IsDeleted == false).FirstOrDefaultAsync(e => e.Id == id);
        if (company == null) throw new KeyNotFoundException($"Entity with ID {id} not found.");
        return company;
    }

    public async Task DeleteCompany(Guid id)
    {
        var company = await GetById(id);
        if (company == null) throw new KeyNotFoundException($"Entity with ID {id} not found.");
        var employeesList = await _empolyeePortalDbContext.Employees.Where(e => e.CompanyId == company.Id).ToListAsync();
        foreach (var employee in employeesList)
        {
            employee.CompanyId = null;
        }
        _empolyeePortalDbContext.Companies.Remove(company);

        await SaveChangesAsync();
    }

    public async Task<List<CompanyEntity>> GetAllAsync()
    {
        var companies = await _empolyeePortalDbContext.Companies.Where(e => e.IsDeleted == false).ToListAsync();
        return companies;
    }

    public async Task<CompanyEntity> GetCompaniyWithEmployeesAsync(Guid id)
    {
        var company = await _empolyeePortalDbContext.Companies.Where(e => e.IsDeleted == false).Include(e => e.Employees).FirstOrDefaultAsync(e => e.Id == id);
        if (company == null) throw new KeyNotFoundException($"Entity with ID {id} not found.");
        return company;
    }


    public async Task<CompanyEntity> UpdateCompany(CompanyEntity entity)
    {
        _empolyeePortalDbContext.Companies.Update(entity);
        await SaveChangesAsync();
        return entity;
    }



    public async Task<int> SaveChangesAsync()
    {
        var currentTime = DateTime.UtcNow.AddHours(4);

        foreach (var entry in _empolyeePortalDbContext.ChangeTracker.Entries().Where(e => e.Entity is CompanyEntity))
        {
            var entityBase = (CompanyEntity)entry.Entity;

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
