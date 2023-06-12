using MeLevaAi.Api.Contracts.Requests.Driver;
using MeLevaAi.Api.Contracts.Requests.DriversLicense;
using MeLevaAi.Api.Contracts.Responses.Driver;
using MeLevaAi.Api.Contracts.Responses.DriversLicense;
using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Mappers
{
    public static class DriversLicenseMapper
    {
        public static DriversLicense ToDriversLicense(this RegisterDriversLicenseRequest request)
            => new(request.Number, request.Category, request.Expiration);

        public static DriversLicenseDto ToDriversLicenseDto(this DriversLicense driversLicense)
        {
            return new DriversLicenseDto
            {
                Id = driversLicense.Id,
                Number = driversLicense.Number,
                Category = driversLicense.Category,
                Expiration = driversLicense.Expiration
            };
        }
    }
}
