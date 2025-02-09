using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;

namespace EmployeePortalWebAPI.Services.IServices;

public interface ICompanyService
{
    Task<List<CompanyEntity>> GetAllAsync();
    Task<CompanyEntity> GetById(Guid id);
    Task<CompanyEntity> GetCompaniyWithEmployeesAsync(Guid id);
    Task<CompanyEntity> UpdateCompany(Guid Id, CompanyDTOes.CompanyUpdateDTO entity);
    Task DeleteCompany(Guid id);
    Task<CompanyEntity> AddCompany(CompanyDTOes.CompanyAddDTO entity);
}
