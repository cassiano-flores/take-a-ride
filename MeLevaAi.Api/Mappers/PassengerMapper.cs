using MeLevaAi.Api.Contracts.Requests.Passenger;
using MeLevaAi.Api.Contracts.Responses.Passenger;
using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Mappers
{
    public static class PassengerMapper
    {
        public static Passenger ToPassenger(this RegisterPassengerRequest request)
            => new(request.Name, request.Cpf, request.Email, request.BirthDate);

        public static PassengerDto ToPassengerDto(this Passenger passenger)
        {
            return new PassengerDto
            {
                Id = passenger.Id,
                VirtualAccount = passenger.VirtualAccount,
                Name = passenger.Name,  
                Cpf = passenger.Cpf,    
                Email = passenger.Email,
                PassengerRating = passenger.PassengerRating,    
                BirthDate = passenger.BirthDate
            };
        }

        public static AddedMoneyDto ToAddedMoneyDto(this Passenger passenger)
        {
            return new AddedMoneyDto
            {
                TotalMoney = passenger.VirtualAccount
            };
        }
    }
}
 