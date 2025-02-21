using AutoMapper;
using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Repositories.IRepositories;
using EmployeePortalWebAPI.Repositories.Repositories;
using EmployeePortalWebAPI.Services.IServices;

namespace EmployeePortalWebAPI.Services.Services;

public class EpicService : IEpicService
{
    private readonly IEpicRepository _epicRepository;
    private readonly IMapper _mapper;
    public EpicService(IEpicRepository epicRepository, IMapper mapper)
    {
        _epicRepository = epicRepository;
        _mapper = mapper;
    }

    public async Task<List<EpicEntity>> GetAllEpicsAsync()
    {
        var epics = await _epicRepository.GetAllEpicsAsync();
        return epics;
    }

    public async Task<EpicEntity> GetEpicByIdAsync(Guid id)
    {
        var epic = await _epicRepository.GetEpicByIdAsync(id);
        return epic;
    }

    public async Task<EpicEntity> UpdateEpicAsync(Guid id, EpicDTOes.UpdateEpicDTO entity)
    {
        var epic = await _epicRepository.GetEpicByIdAsync(id);
        if (epic == null) throw new KeyNotFoundException($"Entity with ID {id} not found.");
        if (entity == null) throw new ArgumentNullException();
        epic.ToDo = entity?.ToDo ?? epic.ToDo;
        epic.EmployeeEntityId = entity?.EmployeeEntityId ?? epic.EmployeeEntityId;
        epic.CompanyEntityId = entity?.CompanyEntityId ?? epic.CompanyEntityId;
        epic.Status = entity?.Status ?? epic.Status;
        await _epicRepository.EditEpicAsync(epic);
        return epic;
    }

    public async Task DeleteEpicAsync(Guid id)
    {
        await _epicRepository.DeleteEpicByIdAsync(id);
    }

    public async Task<EpicEntity> AddEpicAsync(EpicDTOes.AddEpicDTO entity)
    {
        var epicToAdd = new EpicEntity
        {
            ToDo = entity.ToDo,
            EmployeeEntityId = entity.EmployeeEntityId,
            CompanyEntityId = entity.CompanyEntityId,
            Status = entity.Status,
        };
        await _epicRepository.AddEpicAsync(epicToAdd);
        return epicToAdd;
    }
    public async Task<List<EpicEntity>> GetEpicsByCompany(Guid companyId)
    {
        var epics = await _epicRepository.GetEpicsByCompany(companyId);
        return epics;
    }

    public async Task<List<EpicEntity>> GetEpicsByEmployee(Guid employeeId)
    {
        var epics = await _epicRepository.GetEpicsByEmployee(employeeId);
        return epics;
    }

    public async Task<List<EpicEntity>> GetEpicsByStatus(Status status)
    {
        var epics = await _epicRepository.GetEpicsByStatus(status);
        return epics;
    }
}
