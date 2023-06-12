using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Contracts.Responses.DriversLicense
{
    public class DriversLicenseDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Category Category { get; set; }
        public DateOnly Expiration { get; set; }
    }
}
