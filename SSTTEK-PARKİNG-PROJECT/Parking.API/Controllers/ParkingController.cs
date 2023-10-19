using Microsoft.AspNetCore.Mvc;
using Parking.Model.DTOs;
using Parking.Service.Infrastructure;

namespace Parking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpPost("EnterPark")]
        public async Task<IActionResult> EnterPark(VehicleDTO vehicle)
        {
            var result = await _parkingService.EnterPark(vehicle);
            return Ok(result);
        }

        [HttpPost("ExitPark")]
        public async Task<IActionResult> ExitPark(int id, string carParkType)
        {
            var result = await _parkingService.ExitPark(id, carParkType);
            return Ok(result);
        }

        [HttpGet("FetchVehicles")]
        public async Task<IActionResult> FetchVehicles(string carType)
        {
            var result = await _parkingService.FetchVehicles(carType);
            return Ok(result);
        }

        [HttpGet("CalculateHorsePower")]
        public async Task<IActionResult> CalculateHorsePower(int vehicleId)
        {
            var result = await _parkingService.CalculateHorsePower(vehicleId);
            return Ok(result);
        }

        [HttpPost("ExtendStayTime")]
        public async Task<IActionResult> ExtendStayTime(int vehicleId, double howLong)
        {
            var result = await _parkingService.ExtendStayTime(vehicleId, howLong);
            return Ok(result);
        }

        [HttpPost("ChangeTyre")]
        public async Task<IActionResult> ChangeTyre(int vehicleId)
        {
            var result = await _parkingService.ChangeTyre(vehicleId);
            return Ok();
        }

        [HttpPost("WashVehicle")]
        public async Task<IActionResult> WashVehicle(int vehicleId)
        {
            var result = await _parkingService.WashCar(vehicleId);
            return Ok();
        }


    }
}
