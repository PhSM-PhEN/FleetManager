using System.Runtime.CompilerServices;
using AutoMapper;
using FleetManager.communication.Requests;
using FleetManager.communication.Responses;
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

            CreateMap<RequestVehicleUpdateCurrentMileageJson, Vehicle>();

            CreateMap<RequestRegisterUserJson, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<RequestAddressJson, Address>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<RequestClientJson, Client>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<RequestCompanyJson, Company>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());



        }
        private void EntitiesToResponse()
        {
            CreateMap<Category, ResponseCategoryJson>();
            CreateMap<Category, ResponseListCategoryJson>();
            CreateMap<Vehicle, ResponseVehicleJson>();
            CreateMap<Vehicle, ResponseVehicleByIdJson>();
            CreateMap<Vehicle, ResponseRegisterVehicleJson>();
            CreateMap<User, ResponseUserProfileJson>();
            CreateMap<Address, ResponseAddressJson>();
            CreateMap<Address, ResponseShortAddressJson>();
            CreateMap<Client, ResponseShortClientJson>();
            CreateMap<Client, ResponseListClientJson>();
            CreateMap<Client, ResponseClientJson>();
            CreateMap<Company, ResponseCompanyJson>();
            CreateMap<Company, ResponseListCompanyJson>();

        }
    }
}
