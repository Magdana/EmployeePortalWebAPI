﻿using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Services.IServices;
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
    public UserController(IUserService userService)
    {
        _userService = userService;
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
        catch
        {
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
}
