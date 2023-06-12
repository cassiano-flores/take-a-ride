using MeLevaAi.Api.Contracts.Responses.Passenger;
using MeuBlog.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses.Vehicle
{
    public class VehicleResponse : Notifiable
    {
        public VehicleDto? Vehicle { get; set; }
    }
}
