using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortalWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<ActionResult<List<EmployeeEntity>>> GetAllEmployes()
        {
            try
            {
                var employees = await _employeeService.GetAllAsync();
                return Ok(employees);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpGet("{id}")]
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
            catch
            {
                return BadRequest("An error occurred while processing your request.");
            }

        }

        [HttpPost]
        public async Task<ActionResult<EmployeeEntity>> AddEmployee(EmployeeDTOes.EmployeeAddDTO entity)
        {
            try
            {
                var result = await _employeeService.AddEmployee(entity);
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
        public async Task<ActionResult<EmployeeEntity>> UpdateEmployee(Guid id, EmployeeDTOes.EmployeeUpdateDTO entity)
        {
            try
            {
                var updatedEmployee = await _employeeService.UpdateEmployee(id, entity);
                return Ok(updatedEmployee);
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

        public async Task<ActionResult<string>> DeleteEmployee(Guid id)
        {
            try
            {
                await _employeeService.DeleteEmployee(id);
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
}
