namespace Parking.Model.DTOs
{
    public class FetchVehiclesDTO
    {
        public int? Id { get; set; }
        
        public string CarParkType { get; set; }
        
        public string Plate { get; set; } //Plaka
        
        public string Color { get; set; } //Renk
        
        public int ModelYear { get; set; }
        
        public string ModelName { get; set; }
        
        public bool IsPaid { get; set; }
        
        public DateTime EntryTime { get; set; }
        
    }
}
