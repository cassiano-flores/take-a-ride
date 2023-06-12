using MeuBlog.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses.Ride;

public class RideResponse : Notifiable
{
    public RideDto? Ride { get; set; }
}
