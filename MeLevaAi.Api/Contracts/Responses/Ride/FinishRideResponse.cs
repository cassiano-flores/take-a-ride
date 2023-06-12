using MeuBlog.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses.Ride
{
    public class FinishRideResponse : Notifiable
    {
        public FinishRideDto? Ride { get; set; }
    }
}
