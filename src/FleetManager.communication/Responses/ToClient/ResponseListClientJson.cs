using System;

namespace FleetManager.communication.Responses.ToClient;

public class ResponseListClientJson
{
    public List<ResponseShortClientJson> Clients {get ; set ; } = [];
}   
