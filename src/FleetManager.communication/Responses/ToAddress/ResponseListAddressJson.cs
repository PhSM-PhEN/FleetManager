using System;

namespace FleetManager.communication.Responses.ToAddress;

public class ResponseListAddressJson
{
    public List<ResponseShortAddressJson> Address {get; set;} = [];
}
