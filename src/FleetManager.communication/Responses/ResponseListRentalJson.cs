using System;
using FleetManager.Communication.Responses;

namespace FleetManager.Communication.Responses;

public class ResponseListRentalJson
{
    public List<ResponseShortRentalJson> Rentals {get ; set ;} = [];
}
