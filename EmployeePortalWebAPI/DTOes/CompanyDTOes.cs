namespace EmployeePortalWebAPI.DTOes;

public class CompanyDTOes
{
    public class CompanyAddDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
    public class CompanyUpdateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
