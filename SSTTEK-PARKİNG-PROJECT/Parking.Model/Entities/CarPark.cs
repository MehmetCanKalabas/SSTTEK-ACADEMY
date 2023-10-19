using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Model.Entities
{
    public class CarPark
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsOpen { get; set; }
        
        public List<Vehicle> ParkedVehicles { get; set; } // Park eden araçlar
    }
}
