using AutoMapper;
using FleetManager.communication.Requests.ToAddress;
using FleetManager.communication.Requests.ToCategory;
using FleetManager.communication.Requests.ToUser;
using FleetManager.communication.Requests.ToVehicle;
using FleetManager.communication.Resposnes.ToAddress;
using FleetManager.communication.Resposnes.ToCategory;
using FleetManager.communication.Resposnes.ToUsers;
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

            CreateMap<RequestVehicleUpdateCurrentMileageJson, Vehicle>();

            CreateMap<RequestRegisterUserJson, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<RequestAddressJson, Address>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());



        }
        private void EntitiesToResponse()
        {
            CreateMap<Category, ResponseShortCategoryJson>();
            CreateMap<Vehicle, ResponseVehicleJson>();
            CreateMap<Vehicle, ResponseVehicleByIdJson>();
            CreateMap<Vehicle, ResponseRegisterVehicleJson>();
            CreateMap<User, ResponseUserProfileJson>();
            CreateMap<Address, ResponseAddressJson>();


        }
    }
}
