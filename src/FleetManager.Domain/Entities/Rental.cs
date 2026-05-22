using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
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


        public Company Company { get; set; } = default!;
        public Client Client { get; set; } = default!;
        public Vehicle Vehicle { get; set; } = default!;
        public Category Category { get; set; } = default!;
        public User User { get; set; } = default!;
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public RentalStatus Status { get; set; }
        
        


    }
}
