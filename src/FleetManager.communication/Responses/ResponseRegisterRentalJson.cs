
using FleetManager.Communication.ToEnums;

namespace FleetManager.Communication.Responses
{
    public class ResponseRentalRegisterJson
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays { get; set; }
        public long TotalKmAllowed { get; set; }
        public long ExtraKm { get; set; }
        public decimal TotalPrice { get; set; }
        public RentalStatus Status { get; set; }        

    }
}
