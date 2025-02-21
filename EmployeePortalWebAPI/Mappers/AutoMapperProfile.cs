using AutoMapper;
using EmployeePortalWebAPI.DTOes;
using EmployeePortalWebAPI.Entities;

namespace EmployeePortalWebAPI.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<EmployeeEntity, UserDTO.UserInfoDTO>();
        CreateMap<UserEntity, UserDTO.UserEditGetDTO>();
        CreateMap<EmployeeEntity, EmployeeDTOes.EmployeeDetailInfoDTO>();
        CreateMap<EpicEntity, EpicDTOes.GetEpicDTO>();
    }

}
