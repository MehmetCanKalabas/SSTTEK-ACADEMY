using Microsoft.EntityFrameworkCore;
using Parking.Model.Context;
using Parking.Model.DTOs;
using Parking.Model.Entities;
using Parking.Model.Enums;
using Parking.Service.Infrastructure;
using System.Net;

namespace Parking.Service.Services
{
    public class ParkingService : IParkingService
    {
        private readonly MasterDbContext _masterDbContext;

        public ParkingService(MasterDbContext masterDbContext)
        {
            _masterDbContext = masterDbContext;
        }

        public async Task<CustomResponseDto<NoContentDto>> EnterPark(VehicleDTO vehicleDto)
        {
            var carPark = await _masterDbContext.CarParks.FirstOrDefaultAsync();

            if (carPark == null)
            {
                return new CustomResponseDto<NoContentDto> { Errors = new List<string> { "Kayit Bulunamadi" } };
            }
            if (!carPark.IsOpen)
            {
                return new CustomResponseDto<NoContentDto>
                {
                    Errors = new List<string> { "Otopark Kapali" }
                };
            }

            if (vehicleDto.Id != null)
            {
                var vehicle = await _masterDbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == vehicleDto.Id);
                if (vehicle != null)
                {
                    vehicle.HasParked = true;
                    vehicle.EntryTime = DateTime.Now;
                    vehicle.IsPaid = false;
                    _masterDbContext.Vehicles.Update(vehicle);
                    await _masterDbContext.SaveChangesAsync();
                    return new CustomResponseDto<NoContentDto>()
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
            }

            if (string.IsNullOrWhiteSpace(vehicleDto.CarParkType))
            {
                return new CustomResponseDto<NoContentDto>()
                {
                    Errors = new List<string>() { "Arac sınıfı gereklidir." },
                    StatusCode = (int)HttpStatusCode.OK
                };
            }

            if (vehicleDto.CarParkType == CarParkType.FirstClass.ToString())
            {
                var firstClassVehicle = new FirstClassVehicle()
                {
                    CarParkId = carPark.Id,
                    Color = vehicleDto.Color,
                    CarParkType = vehicleDto.CarParkType,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    EntryTime = DateTime.Now,
                    IsPaid = false,
                    ModelName = vehicleDto.ModelName,
                    ModelYear = vehicleDto.ModelYear,
                    Plate = vehicleDto.Plate,
                    Autopilot = vehicleDto.Autopilot,
                    Price = vehicleDto.Price,
                    HasParked = true,
                    StayTime = vehicleDto.StayTime,
                    MotorPowerInKW = vehicleDto.HorsePowerInKW

                };
                await _masterDbContext.FirstClassVehicle.AddAsync(firstClassVehicle);
            }

            else if (vehicleDto.CarParkType == CarParkType.SecondClass.ToString())
            {
                var secondClassVehicle = new SecondClassVehicle()
                {
                    CarParkId = carPark.Id,
                    Color = vehicleDto.Color,
                    CarParkType = vehicleDto.CarParkType,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    EntryTime = DateTime.Now,
                    IsPaid = false,
                    ModelName = vehicleDto.ModelName,
                    ModelYear = vehicleDto.ModelYear,
                    Plate = vehicleDto.Plate,
                    LuggageVolume = vehicleDto.LuggageVolume,
                    HasParked = true,
                    StayTime = vehicleDto.StayTime,
                    MotorPowerInKW = vehicleDto.HorsePowerInKW
                };
                await _masterDbContext.SecondClassVehicle.AddAsync(secondClassVehicle);
            }

            else if (vehicleDto.CarParkType == CarParkType.ThirdClass.ToString())
            {
                var thirdClassVehicle = new ThirdClassVehicle()
                {
                    CarParkId = carPark.Id,
                    Color = vehicleDto.Color,
                    CarParkType = vehicleDto.CarParkType,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    EntryTime = DateTime.Now,
                    IsPaid = false,
                    ModelName = vehicleDto.ModelName,
                    ModelYear = vehicleDto.ModelYear,
                    Plate = vehicleDto.Plate,
                    HasParked = true,
                    StayTime = vehicleDto.StayTime,
                    MotorPowerInKW = vehicleDto.HorsePowerInKW
                };

                await _masterDbContext.ThirdClassVehicle.AddAsync(thirdClassVehicle);
            }

            await _masterDbContext.SaveChangesAsync();

            return new CustomResponseDto<NoContentDto>()
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<CustomResponseDto<NoContentDto>> ExitPark(int vehicleId, string carParkType)
        {
            switch (carParkType)
            {
                case "FirstClass":
                    var firstClassVehicle = await _masterDbContext.FirstClassVehicle.FirstOrDefaultAsync(x => x.Id == vehicleId);
                    if (firstClassVehicle == null)
                    {
                        return new CustomResponseDto<NoContentDto>()
                        {
                            StatusCode = (int)HttpStatusCode.NotFound
                        };
                    }

                    firstClassVehicle.CheckOutTime = DateTime.Now;
                    firstClassVehicle.HasParked = false;
                    firstClassVehicle.IsPaid = true;
                    var totalHours = (DateTime.Now - firstClassVehicle.EntryTime).TotalHours;
                    var hours = totalHours < 1 ? 1 : totalHours;
                    var price = hours * firstClassVehicle.WageCoefficient;
                    firstClassVehicle.ServiceFee = price;

                    _masterDbContext.FirstClassVehicle.Update(firstClassVehicle);
                    await _masterDbContext.SaveChangesAsync();

                    return new CustomResponseDto<NoContentDto>()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                    };

                case "SecondClass":
                    var secondClassVehicle = await _masterDbContext.SecondClassVehicle.FirstOrDefaultAsync(x => x.Id == vehicleId);
                    if (secondClassVehicle == null)
                    {
                        return new CustomResponseDto<NoContentDto>()
                        {
                            StatusCode = (int)HttpStatusCode.NotFound,
                        };
                    }

                    secondClassVehicle.CheckOutTime = DateTime.Now;
                    secondClassVehicle.HasParked = false;
                    secondClassVehicle.IsPaid = true;
                    var sTotalHours = (DateTime.Now - secondClassVehicle.EntryTime).TotalHours;
                    var sHours = sTotalHours < 1 ? 1 : sTotalHours;
                    var sPrice = sHours * secondClassVehicle.WageCoefficient;
                    secondClassVehicle.ServiceFee = sPrice;

                    _masterDbContext.SecondClassVehicle.Update(secondClassVehicle);
                    await _masterDbContext.SaveChangesAsync();

                    return new CustomResponseDto<NoContentDto>()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                    };

                case "ThirdClass":
                    var thirdClassVehicle = await _masterDbContext.ThirdClassVehicle.FirstOrDefaultAsync(x => x.Id == vehicleId);
                    if (thirdClassVehicle == null)
                    {
                        return new CustomResponseDto<NoContentDto>()
                        {
                            StatusCode = (int)HttpStatusCode.NotFound,
                        };
                    }

                    thirdClassVehicle.CheckOutTime = DateTime.Now;
                    thirdClassVehicle.HasParked = false;
                    thirdClassVehicle.IsPaid = true;
                    var tTotalHours = (DateTime.Now - thirdClassVehicle.EntryTime).TotalHours;
                    var tHours = tTotalHours < 1 ? 1 : tTotalHours;
                    var tPrice = tHours * thirdClassVehicle.WageCoefficient;
                    thirdClassVehicle.ServiceFee = tPrice;

                    _masterDbContext.ThirdClassVehicle.Update(thirdClassVehicle);
                    await _masterDbContext.SaveChangesAsync();

                    return new CustomResponseDto<NoContentDto>()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                    };
            }
            return new CustomResponseDto<NoContentDto>();
        }

        public async Task<CustomResponseDto<List<FetchVehiclesDTO>>> FetchVehicles(string carType)
        {
            var vehicles = await _masterDbContext.Vehicles.Where(x => x.HasParked).ToListAsync();

            if (string.IsNullOrWhiteSpace(carType))
            {
                vehicles = vehicles.Where(x => x.CarParkType == carType).ToList();
            }

            var vehicleDTO = vehicles.Select(x => new FetchVehiclesDTO
            {
                Id = x.Id,
                CarParkType = x.CarParkType,
                Plate = x.Plate,
                Color = x.Color,
                ModelYear = x.ModelYear,
                ModelName = x.ModelName,
                IsPaid = x.IsPaid,
                EntryTime = x.EntryTime
            }).ToList();

            return new CustomResponseDto<List<FetchVehiclesDTO>>()
            {
                Data = vehicleDTO,
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<CustomResponseDto<decimal>> CalculateHorsePower(int vehicleId)
        {
            var vehicle = await _masterDbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == vehicleId);

            if (vehicle == null)
            {
                return new CustomResponseDto<decimal>
                {
                    Errors = new List<string> { "Kayit bulunamadi" }
                };
            }

            return new CustomResponseDto<decimal>()
            {
                Data = (decimal)vehicle.MotorPowerInKW * 1.314m
            };
        }

        public async Task<CustomResponseDto<NoContentDto>> ExtendStayTime(int vehicleId, double howLong)
        {
            var vehicle = await _masterDbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == vehicleId);

            if (vehicle == null)
            {
                return new CustomResponseDto<NoContentDto>
                {
                    Errors = new List<string> { "Arac bulunamadi" }
                };
            }

            vehicle.StayTime += howLong;
            await _masterDbContext.SaveChangesAsync();

            return new CustomResponseDto<NoContentDto>
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<CustomResponseDto<NoContentDto>> ChangeTyre(int vehicleId)
        {
            var vehicle = await _masterDbContext.SecondClassVehicle.FirstOrDefaultAsync(x => x.Id == vehicleId);
            if (vehicle == null)
            {
                return new CustomResponseDto<NoContentDto>
                {
                    Errors = new List<string> { "Arac bulunamadi" }
                };
            }

            if (vehicle.TyreChangedVehicle != null)
            {
                return new CustomResponseDto<NoContentDto>
                {
                    Errors = new List<string> { "Lastik degistirilmis" }
                };
            }

            var addedVehicle = new TyreChangedVehicle
            {
                CreatedDate = DateTime.Now,
                VehicleId = vehicleId
            };

            await _masterDbContext.TyreChangedVehicle.AddAsync(addedVehicle);
            await _masterDbContext.SaveChangesAsync();

            return new CustomResponseDto<NoContentDto>
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<CustomResponseDto<NoContentDto>> WashCar(int vehicleId)
        {
            var vehicle = await _masterDbContext.FirstClassVehicle.FirstOrDefaultAsync(x => x.Id == vehicleId);
            if (vehicle == null)
            {
                return new CustomResponseDto<NoContentDto>
                {
                    Errors = new List<string> { "Arac bulunamadi" }
                };
            }

            if (vehicle.WashedVehicle != null)
            {
                return new CustomResponseDto<NoContentDto>
                {
                    Errors = new List<string> { "Arac yikanmis" }
                };
            }

            var addedVehicle = new WashedVehicle
            {
                CreatedDate = DateTime.Now,
                VehicleId = vehicleId
            };

            await _masterDbContext.WashedVehicle.AddAsync(addedVehicle);
            await _masterDbContext.SaveChangesAsync();

            return new CustomResponseDto<NoContentDto>
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
