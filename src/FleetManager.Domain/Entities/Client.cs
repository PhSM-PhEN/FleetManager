using System;

namespace FleetManager.Domain.Entities;

public class Client
{
    public long Id {get; set;}
    public string FirstAndLastName {get; set; } = string.Empty;
    public string RG {get ; set ;} = string.Empty;
    public string CPF {get; set;} = string.Empty;
    public string StatusCivil {get; set;} = string.Empty;

    public Address Address {get; set; } = default!;

}
