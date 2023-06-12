using MeLevaAi.Api.Contracts.Requests.Passenger;
using MeLevaAi.Api.Contracts.Requests.Ride;
using MeLevaAi.Api.Contracts.Responses.Passenger;
using MeLevaAi.Api.Contracts.Responses.Ride;
using MeLevaAi.Api.Domain;
using MeLevaAi.Api.Helpers;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class PassengerService
    {
        private readonly PassengerRepository _passengerRepository;
        private readonly RideRepository _rideRepository;
        private readonly VehicleRepository _vehicleRepository;
        private readonly DriverRepository _driverRepository;
        private readonly DriversLicenseRepository _driversLicenseRepository;
        private readonly RemoveNonNumeric _removeNonNumeric;

        public PassengerService()
        {
            _passengerRepository = new PassengerRepository();
            _rideRepository = new RideRepository();
            _vehicleRepository = new VehicleRepository();
            _driverRepository = new DriverRepository();
            _driversLicenseRepository = new DriversLicenseRepository();
            _removeNonNumeric = new RemoveNonNumeric();
        }

        public List<PassengerDto> List()
        {
            var passengerResponses = new List<PassengerDto>();
            var passengers = _passengerRepository.List();
              
            foreach(var passenger in passengers)
            {
                var passengerResponse = passenger.ToPassengerDto();
                passengerResponses.Add(passengerResponse);
            }

            return passengerResponses;
        }

        public PassengerResponse Get(Guid id)
        {
            var response = new PassengerResponse();
            var passenger = _passengerRepository.Get(id);

            if(passenger is null)
            {
                response.AddNotification(new Validations.Notification("Passenger not found."));
                return response;
            }

            response.Passenger = passenger.ToPassengerDto();

            return response;
        }

        public AddedMoneyResponse AddMoney(AddMoneyRequest request, Guid id)
        {
            var response = new AddedMoneyResponse();
            const int minimalValueToAdd = 0;

            if(request.MoneyToAdd <= minimalValueToAdd)
            {
                response.AddNotification(new Validations.Notification("Money to add must be greater than 0."));
                return response;
            }

            var actualPassenger = _passengerRepository.Get(id);

            if(actualPassenger is null)
            {
                response.AddNotification(new Validations.Notification("Passenger not found."));
                return response;    
            }

            actualPassenger.UpdateVirtualAccount(actualPassenger.VirtualAccount + request.MoneyToAdd);

            response.VirtualAccount = actualPassenger.ToAddedMoneyDto();

            return response;
        }

        public PassengerResponse Register(RegisterPassengerRequest request)
        {
            var response = new PassengerResponse();
            var passengers = _passengerRepository.List();
            request.Cpf = _removeNonNumeric.RemoveNonNumericCharacters(request.Cpf);

            foreach (var passengerList in passengers)
            {
                if(passengerList.Cpf.Equals(request.Cpf))
                {
                    response.AddNotification(new Validations.Notification("There is already a passenger with this CPF."));
                    return response;
                }
            }

            const int MinimalAge = 16;
            const int MaxCpfLenght = 11;

            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            int age = now.Year - request.BirthDate.Year;

            if (request.Cpf.Length > MaxCpfLenght)
            {
                response.AddNotification(new Validations.Notification("The CPF is not valid."));
                return response;
            }

            if (now < request.BirthDate.AddYears(age))
            {
                age--;
            }

            if (age < MinimalAge)
            {
                response.AddNotification(new Validations.Notification("The passenger needs to be at least 16 years old."));
                return response;
            }

            var passenger = request.ToPassenger();
            passenger.UpdateVirtualAccount(0);

            _passengerRepository.Register(passenger);

            response.Passenger = passenger.ToPassengerDto();

            return response;
        }

        public RideResponse RequestRide(RequestRideRequest request)
        {
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            var response = new RideResponse();
            var vehicles = _vehicleRepository.List();

            if (!vehicles.Any())
            {
                response.AddNotification(new Validations.Notification("There are currently no drivers available."));
                return response;
            }
            
            Vehicle vehicle = null;
            List<Vehicle> vehiclesToAssign = vehicles.ToList();

            while (vehicle == null)
            {
                if (!vehiclesToAssign.Any())
                {
                    response.AddNotification(new Validations.Notification("There are currently no drivers available."));
                    return response;
                }
                
                var random = new Random();
                int index = random.Next(0, vehiclesToAssign.Count());

                vehicle = vehiclesToAssign.ElementAt(index);
                var driver = _driverRepository.Get(vehicle.DriverId);
                var driversLicense = _driversLicenseRepository.Get(driver.LicenseId);

                bool isDriverFree = true;
                var rides = _rideRepository.List();

                foreach (var eachRide in rides)
                {
                    var vehicleRide = _vehicleRepository.Get(eachRide.VehicleId);
                    var driverRide = _driverRepository.Get(vehicleRide.DriverId);

                    if ((driver.Id == driverRide.Id) && (eachRide.RideStatus != RideStatus.FINALIZADA))
                    {
                        isDriverFree = false;
                    }
                }   
                
                if ((driversLicense.Expiration > now) && isDriverFree)
                {
                    break;
                }
                else
                {
                    vehiclesToAssign.RemoveAt(index);
                    vehicle = null;
                }
            }
            
            Random randomNumber = new();
            int estimatedArrivalTime = randomNumber.Next(5, 11);
            
            var ride = request.ToRide();

            ride.SetEstimatedArrivalTime(estimatedArrivalTime);

            ride.DriverId = vehicle.DriverId;
            ride.VehicleId = vehicle.Id;

            _rideRepository.RequestRide(ride);

            response.Ride = ride.ToRideDto(vehicle.Id, estimatedArrivalTime, vehicle.DriverId);

            return response;
        }
    }
}
