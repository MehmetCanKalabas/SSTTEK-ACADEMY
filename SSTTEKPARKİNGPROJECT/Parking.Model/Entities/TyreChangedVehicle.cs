namespace Parking.Model.Entities
{
    public class TyreChangedVehicle : Base_Entity
    {
        public int VehicleId { get; set; }
        
        public virtual Vehicle Vehicle { get; set; }
    }
}
