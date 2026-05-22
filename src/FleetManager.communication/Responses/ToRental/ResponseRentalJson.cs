namespace FleetManager.communication.Responses.ToRental
{
    public class ResponseRentalJson
    {
        public class Rental
        {
            public long Id { get; set; }
            public int CompanyId { get; set; }
            public long ClientId { get; set; }
            public long VehicleId { get; set; }
            public int CategoryId { get; set; }
            public long UserId { get; set; }
            public decimal TotalPrice { get; set; }

        }
    }
}
