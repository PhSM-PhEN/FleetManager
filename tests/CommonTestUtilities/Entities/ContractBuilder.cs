using Bogus;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;

namespace CommonTestUtilities.Entities
{
    public class ContractBuilder
    {
        public static List<Contract> Collection(uint count = 3, long? vehicleId = null, long? tenantId = null, RentalPlan? rentalPlan = null)
        {
            var list = new List<Contract>();
            if (count == 0)
                count = 1;
            var contractId = 1;

            for (var i = 0; i < count; i++)
            {
                var contract = Build(vehicleId: vehicleId, tenantId: tenantId, rentalPlan: rentalPlan);
                contract.Id = contractId++;

                list.Add(contract);
            }

            return list;
        }

        public static Contract Build(long? id = null, long? vehicleId = null, long? tenantId = null, RentalPlan? rentalPlan = null,
            RentalType? rentalType = null, ContractStatus status = ContractStatus.Active)
        {
            var plan = rentalPlan ?? RentalPlanBuilder.Build(id: 1);

            var contract = new Faker<Contract>()
                .CustomInstantiator(f =>
                {
                    var type = rentalType ?? f.PickRandom<RentalType>();
                    var startMileage = f.Random.Long(0, 200_000);
                    var totalDays = type == RentalType.Daily ? f.Random.Int(1, 30) : 30;
                    var mileageContracted = type == RentalType.Daily
                        ? plan.MileagePerDay * totalDays
                        : plan.MileagePerMonthly;
                    var totalAmount = type == RentalType.Daily
                        ? plan.DailyPrice * totalDays
                        : plan.MonthlyPrice;
                    var pickupDateTime = f.Date.Soon();
                    var returnDueDateTime = pickupDateTime.AddDays(totalDays);

                    return new Contract(
                        vehicleId ?? f.Random.Long(1, 1000),
                        tenantId ?? f.Random.Long(1, 1000),
                        plan,
                        type,
                        startMileage,
                        mileageContracted,
                        totalAmount,
                        pickupDateTime,
                        returnDueDateTime);
                })
                .Generate();

            if (id.HasValue)
                contract.Id = id.Value;

            if (status == ContractStatus.Active)
                contract.Confirm();
            else if (status == ContractStatus.Cancelled)
                contract.Cancel();
            else if (status == ContractStatus.Finished)
            {
                contract.Confirm();
                contract.Complete(DateTime.UtcNow, contract.EndMileage);
            }
            else if (status == ContractStatus.Overdue)
            {
                contract.Confirm();
                contract.MarkAsOverdue();
            }

            return contract;
        }
    }
}