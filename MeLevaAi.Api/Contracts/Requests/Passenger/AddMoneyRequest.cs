using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests.Passenger
{
    public class AddMoneyRequest
    {
        [Required(ErrorMessage = "The field moneyToAdd is required.")]
        public double MoneyToAdd { get; set; }
    }
}
