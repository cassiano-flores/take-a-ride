using MeLevaAi.Api.Domain;
using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests.Vehicle
{
    public class RegisterVehicleRequest
    {
        [Required(ErrorMessage = "The field plate is required.")]
        public string Plate { get; set; }
        [Required(ErrorMessage = "The field brand is required.")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "The field model is required.")]
        public string Model { get; set; }
        [Required(ErrorMessage = "The field year is required.")]
        public int Year { get; set; }
        [Required(ErrorMessage = "The field driverId is required.")]
        public Guid DriverId { get; set; }
        [Required(ErrorMessage = "The field category is required.")]
        public int Category { get; set; }
        public string? Picture { get; set; }
        public string? Color { get; set; }   
        public int NumberOfSeats { get; set; }
    }
}
