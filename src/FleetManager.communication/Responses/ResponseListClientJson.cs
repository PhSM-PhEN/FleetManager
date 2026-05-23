using System;

namespace FleetManager.communication.Responses;

public class ResponseListClientJson
{
    public List<ResponseShortClientJson> Clients {get ; set ; } = [];
}   
