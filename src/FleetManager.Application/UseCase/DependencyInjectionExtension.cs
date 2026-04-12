using FleetManager.Application.AutoMapper;
using FleetManager.Application.UseCase.ToCategory.Delete;
using FleetManager.Application.UseCase.ToCategory.GetAll;
using FleetManager.Application.UseCase.ToCategory.GetById;
using FleetManager.Application.UseCase.ToCategory.Register;
using FleetManager.Application.UseCase.ToCategory.Update;
using FleetManager.Application.UseCase.ToVehicle.Delete;
using FleetManager.Application.UseCase.ToVehicle.GetAll;
using FleetManager.Application.UseCase.ToVehicle.GetById;
using FleetManager.Application.UseCase.ToVehicle.Register;
using FleetManager.Application.UseCase.ToVehicle.Update;
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
        {
            services.AddScoped<IRegisterCategoryUseCase, RegisterCategoryUseCase>();
            services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();
            services.AddScoped<IGetAllCategoyUseCase, GetAllCategoyUseCase>();
            services.AddScoped<IGetByIdCategoryUseCase, GetByIdCategoryUseCase>();
            services.AddScoped<IDeleteCategoryUseCase, DeleteCategoryUseCase>();

            services.AddScoped<IGetAllVehicleUseCase, GetAllVehicleUseCase>();
            services.AddScoped<IGetByIdVehicleUseCase, GetByIdVehicleUseCase>();            
            services.AddScoped<IRegisterVehicleUseCase, RegisterVehicleUseCase>();
            services.AddScoped<IUpdateVehicleUseCase, UpdateVehicleUseCase>();
            services.AddScoped<IDeleteVehicleUseCase, DeleteVehicleUseCase>();
        }
    }
}
