﻿using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeePortalWebAPI.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeeController> _logger;
    public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;

    }

    [HttpGet]
    [Authorize(Roles = "User,Manager,Admin")]
    public async Task<ActionResult<List<EmployeeEntity>>> GetAllEmployees()
    {
        try
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }
        catch(Exception ex) 
        {
            _logger.LogError(ex, "Error occurred while getting all employess.");
            return StatusCode(500);
        }

    }

    [HttpGet]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<ActionResult<List<EmployeeEntity>>> GetTopTenEarliestEmpolyeesAsync()
    {
        try
        {
            var employees = await _employeeService.GetTopTenEarliestEmpolyessAsync();
            return Ok(employees);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting top earliest employess.");
            return StatusCode(500);
        }

    }

    [HttpGet]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<ActionResult<List<EmployeeEntity>>> GetTopHighSalaryEmployeesAsync()
    {
        try
        {
            var employees = await _employeeService.GetTopHighSalaryEmployeesAsync();
            return Ok(employees);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting top high salary employess.");
            return StatusCode(500);
        }

    }

    [HttpGet]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<ActionResult<List<EmployeeEntity>>> GetSoftDeletedEmployeesAsync()
    {
        try
        {
            var employees = await _employeeService.GetSoftDeletedEmployeesAsync();
            return Ok(employees);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting soft deleted employees.");
            return StatusCode(500);
        }

    }

    [HttpGet("{id}")]
    [Authorize(Roles = "User,Manager,Admin")]
    public async Task<ActionResult<EmployeeEntity>> GetEmployeeById(Guid id)
    {
        try
        {
            var employee = await _employeeService.GetById(id);
            return Ok(employee);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting employee by id.");
            return BadRequest("An error occurred while processing your request.");
        }

    }

    [HttpPost]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<ActionResult<EmployeeEntity>> AddEmployee(EmployeeDTOes.EmployeeAddDTO entity)
    {
        try
        {
            var result = await _employeeService.AddEmployee(entity);
            return Ok(result);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<ActionResult<EmployeeEntity>> UpdateEmployee(Guid id, EmployeeDTOes.EmployeeUpdateDTO entity)
    {
        try
        {
            var updatedEmployee = await _employeeService.UpdateEmployee(id, entity);
            return Ok(updatedEmployee);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("An error occurred while processing your request.");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager,Admin")]

    public async Task<ActionResult<string>> DeleteEmployee(Guid id)
    {
        try
        {
            await _employeeService.DeleteEmployee(id);
            return Ok("Deleted successfully!");
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound();
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("An error occurred while processing your request.");
        }
    }
}
