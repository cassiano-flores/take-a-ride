using MeuBlog.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses.Passenger
{
    public class PassengerResponse : Notifiable
    {
        public PassengerDto? Passenger { get; set; }
    }
}
