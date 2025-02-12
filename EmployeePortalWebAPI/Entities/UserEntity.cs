using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EmployeePortalWebAPI.Entities;

public class UserEntity
{
    [Key]
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    public Guid? EmployeeId { get; set; }
    public EmployeeEntity? Employee { get; set; }
}
public enum UserRole
{
    [EnumMember(Value = "User")]
    User,

    [EnumMember(Value = "Manager")]
    Manager,

    [EnumMember(Value = "Admin")]
    Admin
}
