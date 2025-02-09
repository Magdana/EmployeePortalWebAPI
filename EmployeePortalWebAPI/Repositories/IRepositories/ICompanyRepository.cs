using EmployeePortalWebAPI.Entities;

namespace EmployeePortalWebAPI.Repositories.IRepositories;

public interface ICompanyRepository
{
    Task<List<CompanyEntity>> GetAllAsync();
    Task<CompanyEntity> GetById(Guid id);
    Task<CompanyEntity> GetCompaniyWithEmployeesAsync(Guid id);
    Task<CompanyEntity> UpdateCompany(CompanyEntity entity);
    Task DeleteCompany(Guid id);
    Task AddCompany(CompanyEntity entity);
    Task<int> SaveChangesAsync();
}
