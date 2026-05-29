using System;
using FleetManager.communication.Responses;

namespace FleetManager.Communication.Responses;

public class ResponseRentalInfoJson
{
    public ResponseCompanyJson Company {get ; set;} = new();
    public ResponseClientJson Client {get ; set ;} = new();
}
