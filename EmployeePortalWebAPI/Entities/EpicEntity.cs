using System.Runtime.Serialization;

namespace EmployeePortalWebAPI.Entities;

public class EpicEntity
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
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

}
public enum Status
{
    ToDo,
    InProgress,
    Blocked,
    Completed,
    Cancelled
}