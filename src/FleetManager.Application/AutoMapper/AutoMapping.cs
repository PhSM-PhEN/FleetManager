using AutoMapper;
using FleetManager.communication.Requests.ToCategory;
using FleetManager.communication.Requests.ToVehicle;
using FleetManager.communication.Resposnes.ToCategory;
using FleetManager.communication.Resposnes.ToVehicle;
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
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());

            CreateMap<RequestVehicleUpdateCurrentMiliageJson, Vehicle>();
                

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
