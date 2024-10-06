using AutoMapper;
using BaristaXpertControl.Application.Common.Responses;
using BaristaXpertControl.Application.Features.StoreManagement.Commands;
using BaristaXpertControl.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BaristaXpertControl.Application.Common.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping giữa Command và Entity        
            CreateMap<Store, StoreResponse>();

            CreateMap<CreateStoreCommand, Store>();

            CreateMap<UpdateStoreCommand, Store>();

        }
    }
}
