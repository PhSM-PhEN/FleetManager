using System;

namespace FleetManager.Communication.Responses;

public class ResponseListAddressJson
{
    public List<ResponseShortAddressJson> Address {get; set;} = [];
}
