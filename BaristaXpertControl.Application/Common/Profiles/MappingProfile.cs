using AutoMapper;
using BaristaXpertControl.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Commands;
using BaristaXpertControl.Application.Common.Responses;

namespace BaristaXpertControl.Application.Common.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping giữa Command và Entity
            CreateMap<CreateRoleCommand, Role>();
            CreateMap<Role, RoleResponse>();
        }
    }
}
