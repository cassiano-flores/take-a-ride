using MeuBlog.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses.Driver
{
    public class DriverResponse : Notifiable
    {
        public DriverDto? Driver { get; set; }
    }
}
