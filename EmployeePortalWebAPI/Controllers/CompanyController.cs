using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Services.IServices;
using EmployeePortalWebAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text.Json;

namespace EmployeePortalWebAPI.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;
    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<ActionResult<List<CompanyEntity>>> GetAllCompanies()
    {
        try
        {
            var companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }
        catch
        {
            return StatusCode(500);
        }

    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<ActionResult<CompanyEntity>> GetCompanyById(Guid id)
    {
        try
        {
            var company = await _companyService.GetById(id);
            return Ok(company);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch
        {
            return BadRequest("An error occurred while processing your request.");
        }

    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCompaniyWithEmployeesAsync(Guid id)
    {
        try
        {
            var company = await _companyService.GetCompaniyWithEmployeesAsync(id);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true
            };
            string serialized = JsonSerializer.Serialize(company, options);
            return Ok(serialized);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch
        {
            return BadRequest("An error occurred while processing your request.");
        }
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CompanyEntity>> AddCompany(CompanyDTOes.CompanyAddDTO entity)
    {
        try
        {
            var result = await _companyService.AddCompany(entity);
            return Ok(result);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CompanyEntity>> UpdateCompany(Guid id, CompanyDTOes.CompanyUpdateDTO entity)
    {
        try
        {
            var updatedompany = await _companyService.UpdateCompany(id, entity);
            return Ok(updatedompany);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while processing your request.");
        }
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]

    public async Task<ActionResult<string>> DeleteCompany(Guid id)
    {
        try
        {
            await _companyService.DeleteCompany(id);
            return Ok("Deleted successfully!");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while processing your request.");
        }
    }
}
