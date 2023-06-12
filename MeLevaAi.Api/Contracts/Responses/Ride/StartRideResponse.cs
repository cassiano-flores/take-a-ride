using MeuBlog.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses.Ride
{
    public class StartRideResponse : Notifiable
    {
        public StartRideDto? Ride { get; set; }
    }
}
