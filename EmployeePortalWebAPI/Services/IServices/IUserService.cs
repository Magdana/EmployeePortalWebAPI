using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;
using static EmployeePortalWebAPI.DTOes.EmployeeDTOes;

namespace EmployeePortalWebAPI.Services.IServices;

public interface IUserService
{
    Task<UserEntity?> AuthenticateAsync(string username, string password);
    Task<UserEntity> RegisterAsync(UserDTO.UserRegisterDTO request, UserRole role);
    Task<List<UserDTO.UserInfoDTO>> GetAllUsersAsync();
    Task<EmployeeDetailInfoDTO> GetUserDetailedInfoAsync(string id);
}
