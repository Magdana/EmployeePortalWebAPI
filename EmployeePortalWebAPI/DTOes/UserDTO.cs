﻿namespace EmployeePortalWebAPI.DTOes;

public class UserDTO
{
    public class UserRegisterDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid? EmployeeEntityId { get; set; }
    }
    public class UserLoginDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class UserInfoDTO
    {
        public string? Name { get; set; }
    }
}
