using EmployeePortalWebAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace EmployeePortalWebAPI.DTOes;

public class EmployeeDTOes
{
    public class EmployeeAddDTO
    {
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public Guid? CompanyId { get; set; }
    }
    public class EmployeeUpdateDTO
    {
        public string? Name { get; set; } = string.Empty;
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public decimal? Salary { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
