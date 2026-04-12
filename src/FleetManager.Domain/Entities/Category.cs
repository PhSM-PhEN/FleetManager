namespace FleetManager.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal BaseDailyRate { get; set; }
       

        public ICollection<Vehicle> Vehicles { get; set; } = [];
    }
}
