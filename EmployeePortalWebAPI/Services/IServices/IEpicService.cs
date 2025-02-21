using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;

namespace EmployeePortalWebAPI.Services.IServices;

public interface IEpicService
{
    Task<List<EpicEntity>> GetAllEpicsAsync();
    Task<EpicEntity> GetEpicByIdAsync(Guid id);
    Task<EpicEntity> UpdateEpicAsync(Guid id, EpicDTOes.UpdateEpicDTO entity);
    Task DeleteEpicAsync(Guid id);
    Task<EpicEntity> AddEpicAsync(EpicDTOes.AddEpicDTO entity);
    Task<List<EpicEntity>> GetEpicsByCompany(Guid companyId);
    Task<List<EpicEntity>> GetEpicsByEmployee(Guid employeeId);
    Task<List<EpicEntity>> GetEpicsByStatus(Status status);
}
