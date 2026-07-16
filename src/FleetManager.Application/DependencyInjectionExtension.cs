using FleetManager.Application.UseCase.DoLogin;
using FleetManager.Application.UseCase.ToAddress.Register;
using FleetManager.Application.UseCase.ToUser.ChangePassword;
using FleetManager.Application.UseCase.ToUser.Delete;
using FleetManager.Application.UseCase.ToUser.GetProfile;
using FleetManager.Application.UseCase.ToUser.Promote;
using FleetManager.Application.UseCase.ToUser.Register;
using FleetManager.Application.UseCase.ToUser.Update;
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
            // adress
            services.AddScoped<IRegisterAddressUseCase, RegisterAddressUseCase>();

        }
    }
}
