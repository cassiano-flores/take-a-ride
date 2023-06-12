using MeLevaAi.Api.Contracts.Requests.Passenger;
using MeLevaAi.Api.Contracts.Requests.Vehicle;
using MeLevaAi.Api.Contracts.Responses.Passenger;
using MeLevaAi.Api.Contracts.Responses.Vehicle;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class VehicleService
    {
        private readonly VehicleRepository _vehicleRepository;
        private readonly DriverRepository _driverRepository;
        private readonly DriversLicenseRepository _driversLicenseRepository;

        public VehicleService()
        {
            _vehicleRepository = new VehicleRepository();   
            _driverRepository = new DriverRepository();
            _driversLicenseRepository = new DriversLicenseRepository();
        }

        public List<VehicleDto> List()
        {
            var vehicleResponses = new List<VehicleDto>();
            var vehicles = _vehicleRepository.List();

            foreach (var vehicle in vehicles)
            {
                var vehicleResponse = vehicle.ToVehicleDto();
                vehicleResponses.Add(vehicleResponse);
            }

            return vehicleResponses;
        }

        public VehicleResponse Get(Guid id)
        {
            var response = new VehicleResponse();
            var vehicle = _vehicleRepository.Get(id);

            if (vehicle is null)
            {
                response.AddNotification(new Validations.Notification("Vehicle not found."));
                return response;
            }

            response.Vehicle = vehicle.ToVehicleDto();

            return response;
        }

        public VehicleResponse Register(RegisterVehicleRequest request)
        {
            var response = new VehicleResponse();
            const int maxNumberOfCategories = 5;
            const int minNumberOfCategories = 0;
            const int plusYearsToInvalidYearOfTheCar = 2;
            const int minCarAge = 2005;

            var driver = _driverRepository.Get(request.DriverId);
                
            var actualYear = DateTime.Now.Year;

            if(request.Year > actualYear + plusYearsToInvalidYearOfTheCar)
            {
                response.AddNotification(new Validations.Notification("Year is not valid."));
                return response;
            }

            if(request.Year < minCarAge)
            {
                response.AddNotification(new Validations.Notification("Year must be greater than 2004."));
                return response;
            }

            if(driver is null)
            {
                response.AddNotification(new Validations.Notification("Driver not found."));
                return response;
            }

            var driverLicense = _driversLicenseRepository.Get(driver.LicenseId);

            if((int)driverLicense.Category != request.Category)
            {
                response.AddNotification(new Validations.Notification("Driver license category must be the same from the vehicle."));
                return response;
            }

            if (request.Category > maxNumberOfCategories)
            {
                response.AddNotification(new Validations.Notification("Invalid category."));
                return response;
            }
            else if (request.Category < minNumberOfCategories)
            {
                response.AddNotification(new Validations.Notification("Invalid category."));
                return response;
            }

            var vehicle = request.ToVehicle();
            _vehicleRepository.Register(vehicle);

            response.Vehicle = vehicle.ToVehicleDto();

            return response;
        }
    }
}
