namespace MeLevaAi.Api.Contracts.Responses.Ride;

public class RideDto
{
    public int Id { get; set; }
    public Guid VehicleId { get; set; }
    public Guid DriverId { get; set; }
    public int RideStatus { get; set; } 
    public int EstimatedArrivalTime { get; set; }
}
