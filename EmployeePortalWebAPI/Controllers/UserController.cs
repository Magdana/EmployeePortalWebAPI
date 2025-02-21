using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Services.IServices;
using EmployeePortalWebAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeePortalWebAPI.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;
    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;

    }


    [HttpGet]
    [Authorize(Roles = "User,Manager,Admin")]
    public async Task<ActionResult<List<UserDTO.UserInfoDTO>>> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpGet]
    public async Task<ActionResult<EmployeeDTOes.EmployeeDetailInfoDTO>> GetMyInfo()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User ID not found in token.");

        var userInfo = await _userService.GetUserDetailedInfoAsync(userId);

        if (userInfo == null)
            return NotFound("User not found.");

        return Ok(userInfo);
    }

    [HttpDelete]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<ActionResult<string>> DeleteUser(Guid id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
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

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserDTO.UserEditGetDTO>> UpdateUserRole(string userName, UserDTO.UserEditDTO user)
    {
        try
        {
            var updatedUser = await _userService.EditUserAsync(userName, user);
            return Ok(updatedUser);
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
}
