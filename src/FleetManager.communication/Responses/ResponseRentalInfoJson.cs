using System;
using FleetManager.Communication.Responses;

namespace FleetManager.Communication.Responses;

public class ResponseRentalInfoJson
{
    public ResponseCompanyJson Company {get ; set;} = new();
    public ResponseClientJson Client {get ; set ;} = new();
    public ResponseVehicleByIdJson Vehicle {get ; set ;} = new();
    public ResponseRentalPlanJson RentalPlan {get ; set ;} = new();

}
