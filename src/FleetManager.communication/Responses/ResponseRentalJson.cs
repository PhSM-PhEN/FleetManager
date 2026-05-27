namespace FleetManager.communication.Responses
{
    public class ResponseRentalJson
    {
        public class Rental
        {
            public long Id { get; set; }
            public string CompanyName { get; set; } = string.Empty;
            public string ClientName { get; set; } = string.Empty;
            public string VehicleName { get; set; } = string.Empty;
            public decimal TotalPrice { get; set; }

        }
    }
}
