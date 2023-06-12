using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Contracts.Responses.Vehicle
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Picture { get; set; }
        public int NumberOfSeats { get; set; }
        public Guid DriverId { get; set; }
        public int Category { get; set; }
    }
}
