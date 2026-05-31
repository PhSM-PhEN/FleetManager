namespace FleetManager.Communication.Responses
{
    public class ResponseRentalJson
    {  
            public long Id { get; set; }
            public string CompanyName { get; set; } = string.Empty;
            public string ClientName { get; set; } = string.Empty;
            public string VehicleModel { get; set; } = string.Empty;
            public decimal TotalPrice { get; set; }

    }
}
