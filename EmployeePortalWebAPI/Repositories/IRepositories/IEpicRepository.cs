using EmployeePortalWebAPI.Entities;

namespace EmployeePortalWebAPI.Repositories.IRepositories;

public interface IEpicRepository
{
    Task<List<EpicEntity>> GetAllEpicsAsync();
    Task<EpicEntity> GetEpicByIdAsync(Guid id);
    Task DeleteEpicByIdAsync(Guid id);
    Task<EpicEntity> AddEpicAsync(EpicEntity entity);
    Task<EpicEntity> EditEpicAsync(EpicEntity entity);
    Task<List<EpicEntity>> GetEpicsByCompany(Guid companyId);
    Task<List<EpicEntity>> GetEpicsByEmployee(Guid employeeId);
    Task<List<EpicEntity>> GetEpicsByStatus(Status status);
    Task<int> SaveChangesAsync();

}
