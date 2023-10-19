namespace Parking.Model.Entities
{

    public class FirstClassVehicle : Vehicle
    {
        public virtual WashedVehicle WashedVehicle { get; set; }

        public bool Autopilot { get; set; } //Oto pilot

        public decimal Price { get; set; } // Araç fiyatları

        public double WageCoefficient => 3.0; //Ücret Katsayısı
    }
}
