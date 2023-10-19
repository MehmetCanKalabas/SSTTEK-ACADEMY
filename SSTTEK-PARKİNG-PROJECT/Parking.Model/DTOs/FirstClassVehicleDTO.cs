namespace Parking.Model.DTOs
{
    internal class FirstClassVehicleDTO
    {
        public string Plate { get; set; } //Plaka
        
        public string Color { get; set; } //Renk
        
        public int ModelYear { get; set; }
        
        public string ModelName { get; set; }
        
        public bool IsPaid { get; set; }
        
        public bool Autopilot { get; set; } //Oto pilot
        
        public decimal Price { get; set; } // Araç fiyatları
        
        public int CarWashing { get; set; } // Araba yıkama özelliği
    }
}
