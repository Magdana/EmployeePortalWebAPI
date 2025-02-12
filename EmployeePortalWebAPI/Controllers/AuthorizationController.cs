using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;
using EmployeePortalWebAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeePortalWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{

    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthorizationController(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserEntity>> Register(UserDTO.UserRegisterDTO request, UserRole role = UserRole.User)
    {
        if (!Enum.IsDefined(typeof(UserRole), role))
            return BadRequest("Invalid role");

        var user = await _userService.RegisterAsync(request, role);
        return Ok(user);
    }



    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDTO.UserLoginDTO request)
    {
        var user = await _userService.AuthenticateAsync(request.UserName, request.Password);
        if (user == null) return BadRequest("Invalid username or password.");

        string token = CreateToken(user);
        return Ok(token);
    }

    private string CreateToken(UserEntity user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}

