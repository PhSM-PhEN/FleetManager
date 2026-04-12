using AutoMapper;
using FleetManager.communication.Requests;
using FleetManager.communication.Resposnes;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntities();
            EntitiesToResponse();
        }
        private void RequestToEntities()
        {
            CreateMap<RequestCategoryJson, Category>();
            CreateMap<RequestVehicleJson, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
                

        }
        private void EntitiesToResponse()
        {
            CreateMap<Category, ResponseShortCategoryJson>();
            CreateMap<Vehicle, ResponseVehicleJson>();
            CreateMap<Vehicle, ResponseVehicleByIdJson>();
            CreateMap<Vehicle, ResponseRegisterVehicleJson>();
                
                
        }
    }
}
