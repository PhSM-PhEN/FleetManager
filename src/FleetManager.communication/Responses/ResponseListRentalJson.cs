using System;
using FleetManager.communication.Responses;

namespace FleetManager.Communication.Responses;

public class ResponseListRentalJson
{
    public List<ResponseRentalJson> Rentals {get ; set ;} = [];
}
