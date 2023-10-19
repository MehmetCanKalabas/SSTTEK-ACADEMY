using System.ComponentModel.DataAnnotations;

namespace Parking.Model.DTOs
{
    public class VehicleDTO
    {
        public int? Id { get; set; }
        
        [Required]
        public string CarParkType { get; set; }
        
        [Required]
        public string Plate { get; set; } //Plaka
        
        public string Color { get; set; } //Renk
        
        [Required]
        
        public int ModelYear { get; set; }
        
        [Required]
        
        public string ModelName { get; set; }

        public bool IsPaid { get; set; }
        
        public bool Autopilot { get; set; } //Oto pilot

        public decimal Price { get; set; } // Araç fiyatları
        
        [Required]
        public double StayTime { get; set; } // Kalma Zamani

        public double LuggageVolume { get; set; } // Bagaj Hacmi

        public double HorsePowerInKW { get; set; }

    }
}
