using FleetManager.Application.UseCase.DoLogin;
using FleetManager.Application.UseCase.ToAddress.Delete;
using FleetManager.Application.UseCase.ToAddress.GetAll;
using FleetManager.Application.UseCase.ToAddress.GetById;
using FleetManager.Application.UseCase.ToAddress.Register;
using FleetManager.Application.UseCase.ToAddress.Update;
<<<<<<< HEAD
using FleetManager.Application.UseCase.ToTenant.GetAll;
using FleetManager.Application.UseCase.ToTenant.GetById;
using FleetManager.Application.UseCase.ToTenant.Register;
=======
using FleetManager.Application.UseCase.ToCompany.Delete;
using FleetManager.Application.UseCase.ToCompany.GetAll;
using FleetManager.Application.UseCase.ToCompany.GetById;
using FleetManager.Application.UseCase.ToCompany.Register;
using FleetManager.Application.UseCase.ToCompany.Update;
>>>>>>> 86c49e69cc1a640d657a6d6ed542127f98481fc1
using FleetManager.Application.UseCase.ToUser.ChangePassword;
using FleetManager.Application.UseCase.ToUser.Delete;
using FleetManager.Application.UseCase.ToUser.GetProfile;
using FleetManager.Application.UseCase.ToUser.Promote;
using FleetManager.Application.UseCase.ToUser.Register;
using FleetManager.Application.UseCase.ToUser.Update;
using FleetManager.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManager.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            AddUseCase(services);
        }

        private static void AddUseCase(IServiceCollection services)
        {
            // user 
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IGetProfileUserUseCase, GetProfileUserUseCase>();
            services.AddScoped<IUpdateProfileUserUseCase, UpdateProfileUserUseCase>();
            services.AddScoped<IChangePasswordUseCase, ChangPasswordUseCase>();
            services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            services.AddScoped<IPromoteUserUseCase, PromoteUserUseCase>();
            // login
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            // address
            services.AddScoped<IRegisterAddressUseCase, RegisterAddressUseCase>();
            services.AddScoped<IGetAllAddressUseCase, GetAllAddressUseCase>();
            services.AddScoped<IGetByIdAddressUseCase, GetByIdAddressUseCase>();
            services.AddScoped<IUpdateAddressUseCase, UpdateAddressUseCase>();
            services.AddScoped<IDeleteAddressUseCase, DeleteAddressUseCase>();
<<<<<<< HEAD
            //tenant
            services.AddScoped<IRegisterTenantUseCase, RegisterTenantUseCase>();
            services.AddScoped<IGetAllTenantUseCase , GetAllTenantUseCase>();
            services.AddScoped<IGetByIdTenantUseCase, GetByIdTenantUseCase>();
=======
            // company
            services.AddScoped<IRegisterCompanyUseCase, RegisterCompanyUseCase>();
            services.AddScoped<IGetAllCompanyUseCase, GetAllCompanyUseCase>();
            services.AddScoped<IGetByIdCompanyUseCase, GetByIdCompanyUseCase>();
            services.AddScoped<IUpdateCompanyUseCase, UpdateCompanyUseCase>();
            services.AddScoped<IDeleteCompanyUseCase, DeleteCompanyUseCase>();
>>>>>>> 86c49e69cc1a640d657a6d6ed542127f98481fc1

        }
    }
}
