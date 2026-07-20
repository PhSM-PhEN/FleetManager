namespace FleetManager.Domain.Entities.ValueObjects
{
    public class DriverLicense
    {
        public string Number { get; private set; } = string.Empty;
        public string Category { get; private set; } = string.Empty;

        protected DriverLicense() { }
        public DriverLicense(string number, string category)
        {
            Number = number;
            Category = category;
        }
    }
}
