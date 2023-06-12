using MeLevaAi.Api.Domain;
using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests.DriversLicense
{
    public class RegisterDriversLicenseRequest
    {
        [Required(ErrorMessage = "The field number is required.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "The field category is required.")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "The field expiration is required.")]
        public DateOnly Expiration { get; set; }
    }
}
