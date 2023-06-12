using System.Text.RegularExpressions;
using MeLevaAi.Api.Contracts.Requests.Driver;
using MeLevaAi.Api.Contracts.Requests.DriversLicense;
using MeLevaAi.Api.Contracts.Responses.Driver;
using MeLevaAi.Api.Contracts.Responses.DriversLicense;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class DriversLicenseService
    {
        private readonly DriversLicenseRepository _driversLicenseRepository;

        public DriversLicenseService()
            => _driversLicenseRepository = new();

        public List<DriversLicenseDto> List()
        {
            var driversLicenseResponses = new List<DriversLicenseDto>();
            var driversLicenses = _driversLicenseRepository.List();

            foreach (var driversLicense in driversLicenses)
            {
                var driversLicenseResponse = driversLicense.ToDriversLicenseDto();
                driversLicenseResponses.Add(driversLicenseResponse);
            }

            return driversLicenseResponses;
        }

        public DriversLicenseResponse Get(Guid id)
        {
            var response = new DriversLicenseResponse();
            var driversLicense = _driversLicenseRepository.Get(id);

            if (driversLicense == null)
            {
                response.AddNotification(new Validations.Notification("Drivers License not found."));
                return response;
            }

            response.DriversLicense = driversLicense.ToDriversLicenseDto();

            return response;
        }

        public DriversLicenseResponse Register(RegisterDriversLicenseRequest request)
        {
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            DateOnly minimumValidity = now.AddDays(5);
            
            var response = new DriversLicenseResponse();

            if (request.Expiration < minimumValidity)
            {
                response.AddNotification(new Validations.Notification("The drivers license must be at least 5 days old before it expires."));
                return response;
            }

            var driversLicense = request.ToDriversLicense();
            _driversLicenseRepository.Register(driversLicense);

            response.DriversLicense = driversLicense.ToDriversLicenseDto();

            return response;
        }
    }
}
