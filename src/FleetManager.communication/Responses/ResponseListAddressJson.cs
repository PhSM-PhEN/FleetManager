using System;

namespace FleetManager.communication.Responses;

public class ResponseListAddressJson
{
    public List<ResponseShortAddressJson> Address {get; set;} = [];
}
