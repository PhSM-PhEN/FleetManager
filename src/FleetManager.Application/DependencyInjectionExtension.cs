using FleetManager.Application.UseCase.DoLogin;
using FleetManager.Application.UseCase.ToUser.GetProfile;
using FleetManager.Application.UseCase.ToUser.Register;
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
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IGetProfileUserUseCase, GetProfileUserUseCase>();

            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        }
    }
}
