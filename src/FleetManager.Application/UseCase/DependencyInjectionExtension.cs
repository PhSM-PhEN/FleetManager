using FleetManager.Application.AutoMapper;
using FleetManager.Application.UseCase.ToAddress.Delete;
using FleetManager.Application.UseCase.ToAddress.GetAll;
using FleetManager.Application.UseCase.ToAddress.GetById;
using FleetManager.Application.UseCase.ToAddress.Register;
using FleetManager.Application.UseCase.ToAddress.Update;
using FleetManager.Application.UseCase.ToCategory.Delete;
using FleetManager.Application.UseCase.ToCategory.GetAll;
using FleetManager.Application.UseCase.ToCategory.GetById;
using FleetManager.Application.UseCase.ToCategory.Register;
using FleetManager.Application.UseCase.ToCategory.Update;
using FleetManager.Application.UseCase.ToClient.Delete;
using FleetManager.Application.UseCase.ToClient.GetAll;
using FleetManager.Application.UseCase.ToClient.GetById;
using FleetManager.Application.UseCase.ToClient.Register;
using FleetManager.Application.UseCase.ToClient.Update;
using FleetManager.Application.UseCase.ToCompany.Delete;
using FleetManager.Application.UseCase.ToCompany.GetAll;
using FleetManager.Application.UseCase.ToCompany.GetById;
using FleetManager.Application.UseCase.ToCompany.Register;
using FleetManager.Application.UseCase.ToCompany.Update;
using FleetManager.Application.UseCase.ToLogin;
using FleetManager.Application.UseCase.ToRental.GetAll;
using FleetManager.Application.UseCase.ToRental.GetById;
using FleetManager.Application.UseCase.ToRental.Register;
using FleetManager.Application.UseCase.ToRentalPlan.Delete;
using FleetManager.Application.UseCase.ToRentalPlan.GetAll;
using FleetManager.Application.UseCase.ToRentalPlan.GetById;
using FleetManager.Application.UseCase.ToRentalPlan.Register;
using FleetManager.Application.UseCase.ToRentalPlan.Update;
using FleetManager.Application.UseCase.ToUser.Delete;
using FleetManager.Application.UseCase.ToUser.GetUser;
using FleetManager.Application.UseCase.ToUser.Register;
using FleetManager.Application.UseCase.ToUser.Update;
using FleetManager.Application.UseCase.ToVehicle.Delete;
using FleetManager.Application.UseCase.ToVehicle.GetAll;
using FleetManager.Application.UseCase.ToVehicle.GetById;
using FleetManager.Application.UseCase.ToVehicle.Register;
using FleetManager.Application.UseCase.ToVehicle.Update.UpdateKm;
using FleetManager.Application.UseCase.ToVehicle.Update.UpdateVehicle;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManager.Application.UseCase
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            AddAutoMapper(services);
            AddUseCase(services);
        }
        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(config => config.AddProfile<AutoMapping>());
        }
        private static void AddUseCase(IServiceCollection services)
        {   //Category
            services.AddScoped<IRegisterCategoryUseCase, RegisterCategoryUseCase>();
            services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();
            services.AddScoped<IGetAllCategoryUseCase, GetAllCategoryUseCase>();
            services.AddScoped<IGetByIdCategoryUseCase, GetByIdCategoryUseCase>();
            services.AddScoped<IDeleteCategoryUseCase, DeleteCategoryUseCase>();

            //Vehicle
            services.AddScoped<IRegisterVehicleUseCase, RegisterVehicleUseCase>();
            services.AddScoped<IGetAllVehicleUseCase, GetAllVehicleUseCase>();
            services.AddScoped<IGetByIdVehicleUseCase, GetByIdVehicleUseCase>();            
            services.AddScoped<IUpdateVehicleUseCase, UpdateVehicleUseCase>();
            services.AddScoped<IUpdateVehicleKmUseCase, UpdateVehicleKmUseCase>();
            services.AddScoped<IDeleteVehicleUseCase, DeleteVehicleUseCase>();

            //User
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>(); 
            services.AddScoped<IUpdateProfileUseCase, UpdateProfileUseCase>();
            services.AddScoped<IDeleteUserAccountUseCase, DeleteUserAccountUseCase>();

            //address
            services.AddScoped<IRequestAdressUseCase, RequestAddressUseCase>();
            services.AddScoped<IGetAllAddressUseCase, GetAllAddressUseCase>();
            services.AddScoped<IGetByIdAddressUseCase, GetByIdAddressUseCase>();
            services.AddScoped<IUpdateAddressUseCase, UpdateAddressUseCase>();
            services.AddScoped<IDeleteAddressUseCase, DeleteAddressUseCase>();

            //client 
            services.AddScoped<IRegisterClientUseCase, RegisterClientUseCase>();
            services.AddScoped<IGetAllClientUseCase, GetAllClientUseCase>();
            services.AddScoped<IGetByIdClientUseCase, GetByIdClientUseCase>();
            services.AddScoped<IUpdateClientUseCase, UpdateClientUseCase>();
            services.AddScoped<IDeleteClientUseCase, DeleteClientUseCase>();
            

            // company
            services.AddScoped<IRegisterCompanyUseCase, RegisterCompanyUseCase>();
            services.AddScoped<IGetAllCompanyUseCase, GetAllCompanyUseCase>();
            services.AddScoped<IGetByIdCompanyUseCase, GetByIdCompanyUseCase>();
            services.AddScoped<IUpdateCompanyUseCase, UpdateCompanyUseCase>();
            services.AddScoped<IDeleteCompanyUseCase, DeleteCompanyUseCase>();

            //retal plan
            services.AddScoped<IRegisterRentalPlanUseCase, RegisterRentalPlanUseCase>();
            services.AddScoped<IGetAllRentalPlanUseCase, GetAllRentalPlanUseCase>();
            services.AddScoped<IGetByIdRentalPlanUseCase, GetByIdRentalPlanUseCase>();
            services.AddScoped<IUpdateRentalPlanUseCase, UpdateRentalPlanUseCase>();
            services.AddScoped<IDeleteRentalPlanUseCase, DeleteRentalPlanUseCase>();

            //rental
            services.AddScoped<IRegisterRentalUseCase, RegisterRentalUseCase>();
            services.AddScoped<IGetAllRentalUseCase, GetAllRentalUseCase>();
            services.AddScoped<IGetByIdRentalUseCase, GetByIdRentalUseCase>();

            //Login
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        }
    }
}
