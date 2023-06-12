namespace MeLevaAi.Api.Contracts.Responses.Ride
{
    public class FinishRideDto
    {
        public int Id { get; set; }
        public Guid VehicleId { get; set; }
        public Guid PassengerId { get; set; }
        public Guid DriverId { get; set; }
        public int RideStatus { get; set; }
    }
}
