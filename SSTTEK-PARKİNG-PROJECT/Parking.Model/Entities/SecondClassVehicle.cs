namespace Parking.Model.Entities
{
    public class SecondClassVehicle : Vehicle
    {
        public virtual TyreChangedVehicle TyreChangedVehicle { get; set; }

        public double LuggageVolume { get; set; } // Bagaj Hacmi

        public double WageCoefficient => 2.0; //Ücret Katsayısı
    }
}
