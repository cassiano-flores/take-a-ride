using MeuBlog.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses.Passenger
{
    public class AddedMoneyResponse : Notifiable
    {
        public AddedMoneyDto? VirtualAccount { get; set; }
    }
}
