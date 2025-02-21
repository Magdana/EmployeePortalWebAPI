using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Services.IServices;
using EmployeePortalWebAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Data;

namespace EmployeePortalWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EpicController : ControllerBase
    {
        private readonly ILogger<EpicController> _logger;
        private readonly IEpicService _epicService;

        public EpicController(IEpicService epicService, ILogger<EpicController> logger)
        {
            _epicService = epicService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult<List<EpicEntity>>> GetAllEpicsAsync()
        {
            try
            {
                var epics = await _epicService.GetAllEpicsAsync();
                return Ok(epics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Manager,Admin")]
        public async Task<ActionResult<EpicEntity>> GetEpicByIdAsync(Guid id)
        {
            try
            {
                var epic = await _epicService.GetEpicByIdAsync(id);
                return Ok(epic);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("An error occurred while processing your request.");
            }


        }

        [HttpDelete]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult<string>> DeleteEpicAsync(Guid id)
        {
            try
            {
                await _epicService.DeleteEpicAsync(id);
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

        [HttpPost]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult<EpicEntity>> AddEpicAsync(EpicDTOes.AddEpicDTO entity)
        {
            try
            {
                var result = await _epicService.AddEpicAsync(entity);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult<EpicEntity>> UpdateEpicAsync(Guid id, EpicDTOes.UpdateEpicDTO entity)
        {
            try
            {var epic = await _epicService.UpdateEpicAsync(id, entity);
                return Ok(epic);
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


        [HttpGet]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult<List<EpicEntity>>> GetEpicsByCompany(Guid companyId)
        {
            try
            {
                var epics = await _epicService.GetEpicsByCompany(companyId);
                return Ok(epics);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Authorize(Roles = "User,Manager,Admin")]
        public async Task<ActionResult<List<EpicEntity>>> GetEpicsByEmployee(Guid employeeId)
        {
            try
            {
                var epics = await _epicService.GetEpicsByEmployee(employeeId);
                return Ok(epics);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult<List<EpicEntity>>> GetEpicsByStatus(Status status)
        {
            try
            {
                var epics = await _epicService.GetEpicsByStatus(status);
                return Ok(epics);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("An error occurred while processing your request.");
            }
        }
    }
}
