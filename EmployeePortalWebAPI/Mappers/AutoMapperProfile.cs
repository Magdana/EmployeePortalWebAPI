using AutoMapper;
using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;

namespace EmployeePortalWebAPI.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<EmployeeEntity, UserDTO.UserInfoDTO>();
        CreateMap<EmployeeEntity, EmployeeDTOes.EmployeeDetailInfoDTO>();
    }

}
