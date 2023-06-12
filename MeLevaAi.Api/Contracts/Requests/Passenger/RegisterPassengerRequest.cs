using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests.Passenger
{
    public class RegisterPassengerRequest
    {
        [Required(ErrorMessage = "The field name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field CPF is required.")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "The field birthDate is required.")]
        public DateOnly BirthDate { get; set; }
        [Required(ErrorMessage = "The field email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }
}
