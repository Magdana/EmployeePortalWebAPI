using EmployeePortalWebAPI.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace EmployeePortalWebAPI.DTOes;

public class EpicDTOes
{
    public class UpdateEpicDTO
    {
        public string ToDo { get; set; } = string.Empty;
        public Guid EmployeeEntityId { get; set; }
        public Guid CompanyEntityId { get; set; }
        public Status Status { get; set; } = Status.ToDo;
    }
    public class AddEpicDTO
    {
        public string ToDo { get; set; } = string.Empty;
        public Guid EmployeeEntityId { get; set; }
        public Guid CompanyEntityId { get; set; }
        public Status Status { get; set; } = Status.ToDo;
    }
    public class GetEpicDTO
    {
        public Guid Id { get; set; }
        public string ToDo { get; set; } = string.Empty;
        public Guid EmployeeEntityId { get; set; }
        public EmployeeEntity? Employee { get; set; }
        public Guid CompanyEntityId { get; set; }
        public CompanyEntity? Company { get; set; }
        public Status Status { get; set; } = Status.ToDo;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
