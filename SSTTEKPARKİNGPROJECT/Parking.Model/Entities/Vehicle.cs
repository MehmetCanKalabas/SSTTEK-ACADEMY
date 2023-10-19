using System.ComponentModel.DataAnnotations;

namespace Parking.Model.Entities
{
    public class Vehicle : Base_Entity
    {
        public int? CarParkId { get; set; }
        
        public virtual CarPark CarPark { get; set; }

        public string CarParkType { get; set; } // Discriminator 1,2,3

        [MaxLength(10)]
        public string Color { get; set; } //Renk

        [Required]
        [MaxLength(10)]
        public string Plate { get; set; } //Plaka

        [MaxLength(4)]
        [Required]
        public int ModelYear { get; set; } //Model Yılı

        [Required]
        [MaxLength(20)]
        public string ModelName { get; set; } // Model İsmi

        [Required]
        public DateTime EntryTime { get; set; } // Giriş zamanı

        public DateTime? CheckOutTime { get; set; } // Çıkış zamanı
        
        public double StayTime { get; set; }

        public bool HasParked { get; set; }

        public bool IsPaid { get; set; } //Ödendi mi

        public double ServiceFee { get; set; } // Hizmet Ücreti

        public double MotorPowerInKW { get; set; } // motor gucu

        public virtual double WageCoefficient => 1.0; //Ücret Katsayısı

    }

}
