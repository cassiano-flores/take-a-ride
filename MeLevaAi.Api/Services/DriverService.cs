using MeLevaAi.Api.Contracts.Requests.Driver;
using MeLevaAi.Api.Contracts.Responses.Driver;
using MeLevaAi.Api.Contracts.Responses.Ride;
using MeLevaAi.Api.Domain;
using MeLevaAi.Api.Helpers;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class DriverService
    {
        private readonly DriverRepository _driverRepository;
        private readonly DriversLicenseRepository _driversLicenseRepository;
        private readonly VehicleRepository _vehicleRepository;
        private readonly RemoveNonNumeric _removeNonNumeric;
        private readonly RideRepository _rideRepository;
        private readonly PassengerRepository _passengerRepository;

        const int averageSpeed = 30;
        const int convertSeconds = 3600;
        const double valuePerSecond = 0.20;

        public DriverService()
        {
            _driverRepository = new DriverRepository();
            _driversLicenseRepository = new DriversLicenseRepository();
            _vehicleRepository = new VehicleRepository();
            _removeNonNumeric = new RemoveNonNumeric(); 
            _rideRepository = new RideRepository();
            _passengerRepository = new PassengerRepository();
        }

        public List<DriverDto> List()
        {
            var driverResponses = new List<DriverDto>();
            var drivers = _driverRepository.List();

            foreach (var driver in drivers)
            {
                var driverResponse = driver.ToDriverDto();
                driverResponses.Add(driverResponse);
            }

            return driverResponses;
        }

        public DriverResponse Get(Guid id)
        {
            var response = new DriverResponse();
            var driver = _driverRepository.Get(id);

            if (driver == null)
            {
                response.AddNotification(new Validations.Notification("Driver not found."));
                return response;
            }

            response.Driver = driver.ToDriverDto();

            return response;
        }

        public DriverResponse Register(RegisterDriverRequest request)
        {
            const int MinimalAge = 18;
            const int CpfLenght = 11;
            
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            int age = now.Year - request.BirthDate.Year;
            request.Cpf = _removeNonNumeric.RemoveNonNumericCharacters(request.Cpf);

            var driversLicense = _driversLicenseRepository.Get(request.LicenseId);
            var response = new DriverResponse();
            var drivers = _driverRepository.List();

            foreach (var driverList in drivers)
            {
                if (driverList.Cpf.Equals(request.Cpf))
                {
                    response.AddNotification(new Validations.Notification("There is already a driver with this CPF."));
                    return response;
                }
            }

            if (request.Cpf.Length != CpfLenght)
            {
                response.AddNotification(new Validations.Notification("The CPF is not valid."));
                return response;
            }

            if (now < request.BirthDate.AddYears(age))
                age--;

            if (age < MinimalAge)
            {
                response.AddNotification(new Validations.Notification("The driver needs to be at least 18 years old."));
                return response;
            }
            
            if (driversLicense == null)
            {
                response.AddNotification(new Validations.Notification("The drivers license does not exist."));
                return response;
            }
            
            if (driversLicense.Expiration < now)
            {
                response.AddNotification(new Validations.Notification("The drivers license must not be expired."));
                return response;
            }

            var driver = request.ToDriver();

            driver.UpdateVirtualAccount(0);

            _driverRepository.Register(driver);

            response.Driver = driver.ToDriverDto();

            return response;
        }
        
        public DriverResponse Remove(Guid id)
        {
            var response = new DriverResponse();
            var driver = _driverRepository.Get(id);
            var vehicles = _vehicleRepository.List();
            
            if (driver == null)
            {
                response.AddNotification(new Validations.Notification("Driver not found."));
                return response;
            }
            
            foreach (var vehicle in vehicles)
            {
                if (vehicle.DriverId == driver.Id)
                {
                    response.AddNotification(new Validations.Notification("The driver is linked to a vehicle."));
                    return response;
                }
            }
            
            _driverRepository.Remove(id);
            return response;
        }

        public WithdrawnMoneyResponse WithdrawMoney(WithdrawMoneyRequest request, Guid id)
        {
            var response = new WithdrawnMoneyResponse();
            const int minimalValueToWithdraw = 0;

            if (request.MoneyToWithdraw <= minimalValueToWithdraw)
            {
                response.AddNotification(new Validations.Notification("Money to withdraw must be greater than 0."));
                return response;
            }

            var actualDriver = _driverRepository.Get(id);

            if (actualDriver is null)
            {
                response.AddNotification(new Validations.Notification("Passenger not found."));
                return response;
            }

            if (request.MoneyToWithdraw > actualDriver.VirtualAccount)
            {
                response.AddNotification(new Validations.Notification("The withdraw value must be less than the virtual account."));
                return response;
            }

            if(actualDriver.VirtualAccount < 0)
            {
                actualDriver.UpdateVirtualAccount(0);
            }

            actualDriver.UpdateVirtualAccount(actualDriver.VirtualAccount - request.MoneyToWithdraw);

            response.VirtualAccount = actualDriver.ToWithdrawnMoneyDto();

            return response;
        }

        public StartRideResponse StartRide(int corridaId)
        {
            var ride = _rideRepository.Get(corridaId);
            var response = new StartRideResponse();

            if (ride is null)
            {
                response.AddNotification(new Validations.Notification("Ride not found."));
                return response;
            }

            if(ride.RideStatus != 0)
            {
                response.AddNotification(new Validations.Notification("This ride cannot start."));
                return response;
            }

            double distance = ride.CalculateRideDistance(ride);
            ride.SetStartedRideDate(ride);

            double totalEstimatedRideTime = CalculateTotalEstimatedTimeForTheRide(distance);
            double totalEstimatedRideValue = CalculateTotalEstimatedRideValue(ride.EstimatedArrivalTime);
            ride.SetTimeAndRideValue(ride, totalEstimatedRideTime, totalEstimatedRideValue);

            ride.SetInitiatedRide(ride);
            
            response.Ride = ride.ToStartRideDto();

            return response;
        }

        public FinishRideResponse FinishRide(int corridaId)
        {
            var ride = _rideRepository.Get(corridaId);
            var response = new FinishRideResponse();

            if (ride is null)
            {
                response.AddNotification(new Validations.Notification("Ride not found."));
                return response;
            }

            if((int)ride.RideStatus != 1)
            {
                response.AddNotification(new Validations.Notification("This ride was not initiated."));
                return response;
            }

            ride.SetFinishedRideDate(ride);

            double rideValue = CalculateFinalValueOfTheRide(ride.StartedRide, ride.FinishedRide);

            var passenger = _passengerRepository.Get(ride.PassengerId);

            if (passenger is null)
            {
                response.AddNotification(new Validations.Notification("Passenger not found."));
                return response;
            }

            if (passenger.VirtualAccount < rideValue)
            {
                response.AddNotification(new Validations.Notification("Passenger don't have enough money."));
                return response;
            }

            var driver = _driverRepository.Get(ride.DriverId);

            if (driver is null)
            {
                response.AddNotification(new Validations.Notification("Driver not found."));
                return response;
            }

            passenger.UpdateVirtualAccount(passenger.VirtualAccount - rideValue);
            driver.UpdateVirtualAccount(driver.VirtualAccount + rideValue);

            ride.SetFinishedRide(ride);

            response.Ride = ride.ToFinishRideDto();

            return response;
        }

        public double CalculateFinalValueOfTheRide(DateTime startedRide, DateTime finishedRide)
        {
            TimeSpan diffInSeconds = finishedRide.Subtract(startedRide);
            int seconds = (int)diffInSeconds.TotalSeconds;

            return seconds * valuePerSecond;
        }

        public double CalculateTotalEstimatedTimeForTheRide(double distance)
        {
            return (distance / averageSpeed) * convertSeconds;
        }

        public double CalculateTotalEstimatedRideValue(int rideTime)
        {
            const int MINUTES_TO_SECONDS = 60;
            return (rideTime * MINUTES_TO_SECONDS) * valuePerSecond;
        }
    }
}
