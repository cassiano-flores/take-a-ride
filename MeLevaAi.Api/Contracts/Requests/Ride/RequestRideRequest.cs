using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests.Ride;

public class RequestRideRequest
{
    [Required(ErrorMessage = "The field passenger id is required.")]
    public Guid PassengerId { get; set; }

    [Required(ErrorMessage = "The field initial point x is required.")]
    public double InitialPointX { get; set; }

    [Required(ErrorMessage = "The field initial point y is required.")]
    public double InitialPointY { get; set; }
    
    [Required(ErrorMessage = "The field final point x is required.")]
    public double FinalPointX { get; set; }
    
    [Required(ErrorMessage = "The field final point y is required.")]
    public double FinalPointY { get; set; }
}
