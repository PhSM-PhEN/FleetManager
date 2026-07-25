using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToVehicle;
using FleetManager.Application.UseCase.ToVehicle.Register;
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
            var request = RequestVehicleJsonBuilder.Build(company.Id);

            var useCase = CreateUseCase(company: company);
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Model.ShouldBe(request.Model);
            result.CurrentMileage.ShouldBe(request.CurrentMileage);
        }

        [Fact]
        public async Task Error_Brand_Empty()
        {
            var company = CompanyBuilder.Build();
            var request = RequestVehicleJsonBuilder.Build(company.Id);
            request.Brand = string.Empty;

            var useCase = CreateUseCase(company: company);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.BRAND_REQUIRED);
        }

        [Fact]
        public async Task Error_Model_Empty()
        {
            var company = CompanyBuilder.Build();
            var request = RequestVehicleJsonBuilder.Build(company.Id);
            request.Model = string.Empty;

            var useCase = CreateUseCase(company: company);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.MODEL_REQUIRED);
        }

        [Fact]
        public async Task Error_Mileage_Negative()
        {
            var company = CompanyBuilder.Build();
            var request = RequestVehicleJsonBuilder.Build(company.Id);
            request.CurrentMileage = -1;

            var useCase = CreateUseCase(company: company);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.MILEAGE_INVALID);
        }

        [Fact]
        public async Task Error_CompanyId_Zero()
        {
            var request = RequestVehicleJsonBuilder.Build(0);

            var useCase = CreateUseCase(company: null);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.COMPANY_ID_REQUIRED);
        }

        [Fact]
        public async Task Error_Company_Not_Found()
        {
            var request = RequestVehicleJsonBuilder.Build(999);

            var useCase = CreateUseCase(company: null);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.COMPANY_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Renavam_Invalid()
        {
            var company = CompanyBuilder.Build();
            var request = RequestVehicleJsonBuilder.Build(company.Id);
            request.Renavam = "00000000000"; // dígito verificador inválido

            var useCase = CreateUseCase(company: company);
            var act = async () => await useCase.Execute(request);

            await act.ShouldThrowAsync<ErrorOnValidationException>();
        }

        [Fact]
        public async Task Error_LicensePlate_Invalid()
        {
            var company = CompanyBuilder.Build();
            var request = RequestVehicleJsonBuilder.Build(company.Id);
            request.LicensePlate = "1234";

            var useCase = CreateUseCase(company: company);
            var act = async () => await useCase.Execute(request);

            await act.ShouldThrowAsync<ErrorOnValidationException>();
        }

        [Fact]
        public async Task Error_ManufacturingYear_Format_Invalid()
        {
            var company = CompanyBuilder.Build();
            var request = RequestVehicleJsonBuilder.Build(company.Id);
            request.ManufacturingYear = "invalid-year";

            var useCase = CreateUseCase(company: company);
            var act = async () => await useCase.Execute(request);

            await act.ShouldThrowAsync<ErrorOnValidationException>();
        }

        private static RegisterVehicleUseCase CreateUseCase(FleetManager.Domain.Entities.Company? company)
        {
            var writeRepository = new VehicleWriteOnlyRepositoryBuilder().Build();

            var companyRepository = new CompanyReadOnlyRepositoryBuilder()
                .GetById(company, company?.Id ?? 999)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterVehicleUseCase(writeRepository, companyRepository, unitOfWork);
        }
    }
}
