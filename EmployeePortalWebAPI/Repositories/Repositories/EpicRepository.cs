using EmployeePortalWebAPI.DAL;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalWebAPI.Repositories.Repositories;

public class EpicRepository : IEpicRepository
{
    private readonly EmpolyeePortalDbContext _context;
    public EpicRepository(EmpolyeePortalDbContext context)
    {
        _context = context;
    }
    public async Task<List<EpicEntity>> GetAllEpicsAsync()
    {
        var epics = await _context.Epics.Where(e => e.IsDeleted == false).ToListAsync();
        return epics;
    }
    public async Task<EpicEntity> GetEpicByIdAsync(Guid id)
    {
        var epic = await _context.Epics.Where(e => e.IsDeleted == false).FirstOrDefaultAsync(e => e.Id == id);
        if (epic == null) throw new KeyNotFoundException($"Entity with ID {id} not found.");
        return epic;
    }
    public async Task DeleteEpicByIdAsync(Guid id)
    {
        var epicToDelete = await GetEpicByIdAsync(id);
        if (epicToDelete == null) throw new KeyNotFoundException($"Entity with ID {id} not found.");
        _context.Epics.Remove(epicToDelete);
        await SaveChangesAsync();
    }

    public async Task<EpicEntity> AddEpicAsync(EpicEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        await _context.Epics.AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    public async Task<EpicEntity> EditEpicAsync(EpicEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        _context.Epics.Update(entity);
        await SaveChangesAsync();
        return entity;
    }

    public async Task<List<EpicEntity>> GetEpicsByCompany(Guid companyId)
    {
        var epics = await _context.Epics.Where(e => e.IsDeleted == false && e.CompanyEntityId == companyId).ToListAsync();
        return epics;
    }

    public async Task<List<EpicEntity>> GetEpicsByEmployee(Guid employeeId)
    {

        var epics = await _context.Epics.Where(e => e.IsDeleted == false && e.EmployeeEntityId == employeeId).ToListAsync();
        return epics;
    }
    public async Task<List<EpicEntity>> GetEpicsByStatus(Status status)
    {
        var epics = await _context.Epics.Where(e => e.IsDeleted == false && e.Status == status).ToListAsync();
        return epics;
    }

    public async Task<int> SaveChangesAsync()
    {
        var currentTime = DateTime.UtcNow.AddHours(4);

        foreach (var entry in _context.ChangeTracker.Entries().Where(e => e.Entity is EpicEntity))
        {
            var entityBase = (EpicEntity)entry.Entity;

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
        return await _context.SaveChangesAsync();
    }
}
