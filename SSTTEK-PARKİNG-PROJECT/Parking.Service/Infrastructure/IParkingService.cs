using Parking.Model.DTOs;

namespace Parking.Service.Infrastructure
{
    public interface IParkingService
    {
        Task<CustomResponseDto<NoContentDto>> EnterPark(VehicleDTO vehicle);
        
        Task<CustomResponseDto<NoContentDto>> ExitPark(int vehicle, string carParkType);

        Task<CustomResponseDto<List<FetchVehiclesDTO>>> FetchVehicles(string carType);

        Task<CustomResponseDto<decimal>> CalculateHorsePower(int vehicleId);

        Task<CustomResponseDto<NoContentDto>> ExtendStayTime(int vehicleId, double howMany);
        
        Task<CustomResponseDto<NoContentDto>> ChangeTyre(int vehicleId);

        Task<CustomResponseDto<NoContentDto>> WashCar(int vehicleId);

    }
}
