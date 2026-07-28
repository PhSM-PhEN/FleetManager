using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToCompany;
using CommonTestUtilities.Repositories.ToRentalPlan;
using CommonTestUtilities.Repositories.ToVehicle;
using CommonTestUtilities.Request.ToVehicle;
using FleetManager.Application.UseCase.ToVehicle.Register;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToVehicle.Register
{
    public class RegisterVehicleUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var company = CompanyBuilder.Build(1, 1);
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestVehicleJsonBuilder.Build(company.Id, rentalPlan.Id);   // 🔧 passa o id do plano

            var useCase = CreateUseCase(company: company, rentalPlan: rentalPlan);       // 🔧 passa o plano pro use case
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Model.ShouldBe(request.Model);
            result.CurrentMileage.ShouldBe(request.CurrentMileage);
        }

        [Fact]
        public async Task Error_Brand_Empty()
        {
            var company = CompanyBuilder.Build();
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestVehicleJsonBuilder.Build(company.Id, rentalPlan.Id);
            request.Brand = string.Empty;

            var useCase = CreateUseCase(company: company, rentalPlan: rentalPlan);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.BRAND_REQUIRED);
        }

        [Fact]
        public async Task Error_Model_Empty()
        {
            var company = CompanyBuilder.Build();
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestVehicleJsonBuilder.Build(company.Id, rentalPlan.Id);
            request.Model = string.Empty;

            var useCase = CreateUseCase(company: company, rentalPlan: rentalPlan);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.MODEL_REQUIRED);
        }

        [Fact]
        public async Task Error_Mileage_Negative()
        {
            var company = CompanyBuilder.Build();
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestVehicleJsonBuilder.Build(company.Id, rentalPlan.Id);
            request.CurrentMileage = -1;

            var useCase = CreateUseCase(company: company, rentalPlan: rentalPlan);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.MILEAGE_INVALID);
        }

        [Fact]
        public async Task Error_CompanyId_Zero()
        {
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestVehicleJsonBuilder.Build(0, rentalPlan.Id);

            var useCase = CreateUseCase(company: null, rentalPlan: rentalPlan);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.COMPANY_ID_REQUIRED);
        }

        [Fact]
        public async Task Error_Company_Not_Found()
        {
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestVehicleJsonBuilder.Build(999, rentalPlan.Id);

            var useCase = CreateUseCase(company: null, rentalPlan: rentalPlan);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.COMPANY_NOT_FOUND);
        }

        [Fact]
        public async Task Error_RentalPlan_Not_Found()   // 🔧 caso que faltava cobrir explicitamente
        {
            var company = CompanyBuilder.Build(1, 1);
            var request = RequestVehicleJsonBuilder.Build(company.Id, 999);

            var useCase = CreateUseCase(company: company, rentalPlan: null);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Renavam_Invalid()
        {
            var company = CompanyBuilder.Build();
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestVehicleJsonBuilder.Build(company.Id, rentalPlan.Id);
            request.Renavam = "00000000000"; // dígito verificador inválido

            var useCase = CreateUseCase(company: company, rentalPlan: rentalPlan);
            var act = async () => await useCase.Execute(request);

            await act.ShouldThrowAsync<ErrorOnValidationException>();
        }

        [Fact]
        public async Task Error_LicensePlate_Invalid()
        {
            var company = CompanyBuilder.Build();
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestVehicleJsonBuilder.Build(company.Id, rentalPlan.Id);
            request.LicensePlate = "1234";

            var useCase = CreateUseCase(company: company, rentalPlan: rentalPlan);
            var act = async () => await useCase.Execute(request);

            await act.ShouldThrowAsync<ErrorOnValidationException>();
        }

        [Fact]
        public async Task Error_ManufacturingYear_Format_Invalid()
        {
            var company = CompanyBuilder.Build();
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestVehicleJsonBuilder.Build(company.Id, rentalPlan.Id);
            request.ManufacturingYear = "invalid-year";

            var useCase = CreateUseCase(company: company, rentalPlan: rentalPlan);
            var act = async () => await useCase.Execute(request);

            await act.ShouldThrowAsync<ErrorOnValidationException>();
        }

        private static RegisterVehicleUseCase CreateUseCase(Company? company, RentalPlan? rentalPlan)
        {
            var writeRepository = new VehicleWriteOnlyRepositoryBuilder().Build();

            var rentalPlanRepositoryBuilder = new RentalPlanReadOnlyRepositoryBuilder();
            if (rentalPlan is not null)
                rentalPlanRepositoryBuilder.GetById(rentalPlan);           // 🔧 mock agora responde ao id certo
            var rentalPlanRepository = rentalPlanRepositoryBuilder.Build();

            var companyRepository = new CompanyReadOnlyRepositoryBuilder()
                .GetById(company, company?.Id ?? 999)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterVehicleUseCase(writeRepository, companyRepository, rentalPlanRepository, unitOfWork);
        }
    }
}