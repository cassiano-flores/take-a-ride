using MeuBlog.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses.Driver
{
    public class WithdrawnMoneyResponse : Notifiable
    {
        public WithdrawnMoneyDto? VirtualAccount { get; set; }
    }
}
