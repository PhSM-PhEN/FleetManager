using FleetManager.Application.UseCase.DoLogin;
using FleetManager.Application.UseCase.ToAddress.Delete;
using FleetManager.Application.UseCase.ToAddress.GetAll;
using FleetManager.Application.UseCase.ToAddress.GetById;
using FleetManager.Application.UseCase.ToAddress.Register;
using FleetManager.Application.UseCase.ToAddress.Update;
using FleetManager.Application.UseCase.ToCompany.Delete;
using FleetManager.Application.UseCase.ToCompany.GetAll;
using FleetManager.Application.UseCase.ToCompany.GetById;
using FleetManager.Application.UseCase.ToCompany.Register;
using FleetManager.Application.UseCase.ToCompany.Update;
using FleetManager.Application.UseCase.ToContract.Activate;
using FleetManager.Application.UseCase.ToContract.Cancel;
using FleetManager.Application.UseCase.ToContract.Delete;
using FleetManager.Application.UseCase.ToContract.GetAll;
using FleetManager.Application.UseCase.ToContract.GetById;
using FleetManager.Application.UseCase.ToContract.Preview;
using FleetManager.Application.UseCase.ToContract.Register;
using FleetManager.Application.UseCase.ToContract.Update;
using FleetManager.Application.UseCase.ToIncidentReport.Delete;
using FleetManager.Application.UseCase.ToIncidentReport.GetAll;
using FleetManager.Application.UseCase.ToIncidentReport.GetById;
using FleetManager.Application.UseCase.ToIncidentReport.Register;
using FleetManager.Application.UseCase.ToIncidentReport.Resolve;
using FleetManager.Application.UseCase.ToRentalPlan.Delete;
using FleetManager.Application.UseCase.ToRentalPlan.GetAll;
using FleetManager.Application.UseCase.ToRentalPlan.GetById;
using FleetManager.Application.UseCase.ToRentalPlan.Register;
using FleetManager.Application.UseCase.ToRentalPlan.Update;
using FleetManager.Application.UseCase.ToTenant.Delete;
using FleetManager.Application.UseCase.ToTenant.GetAll;
using FleetManager.Application.UseCase.ToTenant.GetById;
using FleetManager.Application.UseCase.ToTenant.Register;
using FleetManager.Application.UseCase.ToTenant.Update;
using FleetManager.Application.UseCase.ToUser.ChangePassword;
using FleetManager.Application.UseCase.ToUser.Delete;
using FleetManager.Application.UseCase.ToUser.GetProfile;
using FleetManager.Application.UseCase.ToUser.Promote;
using FleetManager.Application.UseCase.ToUser.Register;
using FleetManager.Application.UseCase.ToUser.Update;
using FleetManager.Application.UseCase.ToVehicle.Delete;
using FleetManager.Application.UseCase.ToVehicle.GetAll;
using FleetManager.Application.UseCase.ToVehicle.GetById;
using FleetManager.Application.UseCase.ToVehicle.Register;
using FleetManager.Application.UseCase.ToVehicle.Update;
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
            services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
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
            //tenant
            services.AddScoped<IRegisterTenantUseCase, RegisterTenantUseCase>();
            services.AddScoped<IGetAllTenantUseCase , GetAllTenantUseCase>();
            services.AddScoped<IGetByIdTenantUseCase, GetByIdTenantUseCase>();
            services.AddScoped<IUpdateTenantUseCase, UpdateTenantUseCase>();
            services.AddScoped<IDeleteTenantUseCase, DeleteTenantUseCase>();
            // company
            services.AddScoped<IRegisterCompanyUseCase, RegisterCompanyUseCase>();
            services.AddScoped<IGetAllCompanyUseCase, GetAllCompanyUseCase>();
            services.AddScoped<IGetByIdCompanyUseCase, GetByIdCompanyUseCase>();
            services.AddScoped<IUpdateCompanyUseCase, UpdateCompanyUseCase>();
            services.AddScoped<IDeleteCompanyUseCase, DeleteCompanyUseCase>();
            // vehicle
            services.AddScoped<IRegisterVehicleUseCase, RegisterVehicleUseCase>();
            services.AddScoped<IGetAllVehicleUseCase, GetAllVehicleUseCase>();
            services.AddScoped<IGetByIdVehicleUseCase, GetByIdVehicleUseCase>();
            services.AddScoped<IUpdateMileageVehicleUseCase, UpdateMileageVehicleUseCase>();
            services.AddScoped<IDeleteVehicleUseCase, DeleteVehicleUseCase>();
            // rental plan
            services.AddScoped<IRegisterRentalPlanUseCase, RegisterRentalPlanUseCase>();
            services.AddScoped<IUpdateRentalPlanUseCase, UpdateRentalPlanUseCase>();
            services.AddScoped<IGetByRentalPlanUseCase, GetByIdRentalPlanUseCase>();
            services.AddScoped<IGetAllRentalPlanUseCase, GetAllRentalPlanUseCase>();
            services.AddScoped<IDeleteRentalPlanUseCase, DeleteRentalPlanUseCase>();
            // contract
            services.AddScoped<IRegisterContractUseCase, RegisterContractUseCase>();
            services.AddScoped<IGetByIdContractUseCase, GetByIdContractUseCase>();
            services.AddScoped<IGetAllContractUseCase, GetAllContractUseCase>();
            services.AddScoped<IUpdateContractUseCase, UpdateContractUseCase>();
            services.AddScoped<IDeleteContractUseCase, DeleteContractUseCase>();
            services.AddScoped<IPreviewContractUseCase, PreviewContractUseCase>();
            services.AddScoped<ICancelContractUseCase, CancelContractUseCase>();
            services.AddScoped<IActivateContractUseCase, ActivateContractUseCase>();
            // incident report
            services.AddScoped<IRegisterIncidentReportUseCase, RegisterIncidentReportUseCase>();
            services.AddScoped<IGetAllIncidentReportUseCase, GetAllIncidentReportUseCase>();
            services.AddScoped<IGetByIdIncidentReportUseCase, GetByIdIncidentReportUseCase>();
            services.AddScoped<IDeleteIncidentReportUseCase, DeleteIncidentReportUseCase>();
            services.AddScoped<IResolveIncidentReportUseCase, ResolveIncidentReportUseCase>();


        }
    }
}
