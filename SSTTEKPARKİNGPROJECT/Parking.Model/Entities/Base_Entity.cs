using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Model.Entities
{
    public class Base_Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        
        public string CreatedBy { get; set; }


    }
}
