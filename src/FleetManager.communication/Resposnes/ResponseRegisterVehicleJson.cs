using FleetManager.communication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.communication.Resposnes
{
    public class ResponseRegisterVehicleJson
    {
        public long Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public FuelTypeEnum FuelType { get; set; }
        public TransmissionTypeEnum TransmissionType { get; set; }
        public long CurrentMileage { get; set; }
        public int CategoryId { get; set; }

    }
}
