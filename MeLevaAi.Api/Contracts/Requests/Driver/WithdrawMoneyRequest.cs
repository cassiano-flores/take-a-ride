using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests.Driver
{
    public class WithdrawMoneyRequest
    {
        [Required(ErrorMessage = "The field moneyToWithdraw is required.")]
        public double MoneyToWithdraw { get; set; }
    }
}
