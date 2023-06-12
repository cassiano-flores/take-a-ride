using MeLevaAi.Api.Contracts.Requests.Ride;
using MeLevaAi.Api.Contracts.Responses.Ride;
using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Mappers;

public static class RideMapper
{
    public static Ride ToRide(this RequestRideRequest request)
        => new(request.PassengerId, request.InitialPointX, request.InitialPointY, request.FinalPointX, request.FinalPointY);

    public static RideDto ToRideDto(this Ride ride, Guid vehicleId, int estimatedArrivalTime, Guid driverId)
    {
        return new RideDto
        {
            Id = ride.Id,
            VehicleId = vehicleId,
            DriverId = driverId,
            RideStatus = (int)ride.RideStatus,
            EstimatedArrivalTime = estimatedArrivalTime
        };
    }

    public static StartRideDto ToStartRideDto(this Ride ride)
    {
        return new StartRideDto
        {
            EstimatedRideTime = ride.TotalEstimatedRideTime,
            EstimatedRideValue = ride.TotalEstimatedRideValue
        };
    }

    public static FinishRideDto ToFinishRideDto(this Ride ride)
    {
        return new FinishRideDto
        {
            Id = ride.Id,
            VehicleId = ride.VehicleId,
            PassengerId = ride.PassengerId,
            DriverId = ride.DriverId,
            RideStatus = (int)ride.RideStatus
        };
    }
}
