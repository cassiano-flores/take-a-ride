using MeLevaAi.Api.Contracts.Requests.Passenger;
using MeLevaAi.Api.Contracts.Requests.Vehicle;
using MeLevaAi.Api.Contracts.Responses.Passenger;
using MeLevaAi.Api.Contracts.Responses.Vehicle;
using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Mappers
{
    public static class VehicleMapper
    {
        public static Vehicle ToVehicle(this RegisterVehicleRequest request)
            => new(request.Plate, request.Brand, request.Model, request.Year, request.Color, request.Picture,
                request.NumberOfSeats, request.DriverId, (Category)request.Category);

        public static VehicleDto ToVehicleDto(this Vehicle vehicle)
        {
            return new VehicleDto
            {
                Id = vehicle.Id,
                Plate = vehicle.Plate,  
                Model = vehicle.Model,
                Brand = vehicle.Brand,
                Year = vehicle.Year,
                Color = vehicle.Color,
                Picture = vehicle.Picture,
                NumberOfSeats = vehicle.NumberOfSeats,
                DriverId = vehicle.DriverId,
                Category = (int)vehicle.Category,
            };
        }
    }
}
