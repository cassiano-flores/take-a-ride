using MeLevaAi.Api.Domain;
using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests.Driver
{
    public class RegisterDriverRequest
    {
        [Required(ErrorMessage = "The field name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field birthdate is required.")]
        public DateOnly BirthDate { get; set; }

        [Required(ErrorMessage = "The field CPF is required.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "The field license id is required.")]
        public Guid LicenseId { get; set; }
    }
}
