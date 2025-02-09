using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Repositories.IRepositories;
using EmployeePortalWebAPI.Repositories.Repositories;
using EmployeePortalWebAPI.Services.IServices;

namespace EmployeePortalWebAPI.Services.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<CompanyEntity> AddCompany(CompanyDTOes.CompanyAddDTO entity)
    {
        var company = new CompanyEntity
        {
            Name = entity.Name,
            Location = entity.Location
        };
        await _companyRepository.AddCompany(company);
        return await Task.FromResult(company);
    }

    public async Task DeleteCompany(Guid id)
    {
        await _companyRepository.DeleteCompany(id);
    }

    public async Task<List<CompanyEntity>> GetAllAsync()
    {
        var companies = await _companyRepository.GetAllAsync();
        return companies;
    }
    

    public async Task<CompanyEntity> GetById(Guid id)
    {
        var company = await _companyRepository.GetById(id);
        return company;
    }

    public async Task<CompanyEntity> GetCompaniyWithEmployeesAsync(Guid id)
    {
        var company = await _companyRepository.GetCompaniyWithEmployeesAsync(id);
        return company;
    }

    public async Task<CompanyEntity> UpdateCompany(Guid id, CompanyDTOes.CompanyUpdateDTO entity)
    {
        var company = await _companyRepository.GetById(id);
        if (company == null) throw new KeyNotFoundException($"Entity with ID {id} not found.");
        if (entity == null) throw new ArgumentNullException();
        company.Name = entity?.Name ?? company.Name;
        company.Location = entity?.Location ?? company.Location;
        await _companyRepository.UpdateCompany(company);
        return company;
    }
}
