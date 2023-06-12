using MeLevaAi.Api.Contracts.Requests.Driver;
using MeLevaAi.Api.Contracts.Responses.Driver;
using MeLevaAi.Api.Contracts.Responses.Passenger;
using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Mappers
{
    public static class DriverMapper
    {
        public static Driver ToDriver(this RegisterDriverRequest request)
            => new(request.Name, request.Email, request.BirthDate, request.Cpf, request.LicenseId);

        public static DriverDto ToDriverDto(this Driver driver)
        {
            return new DriverDto
            {
                Id = driver.Id,
                Name = driver.Name,
                Email = driver.Email,
                BirthDate = driver.BirthDate,
                Cpf = driver.Cpf,
                LicenseId = driver.LicenseId,
                VirtualAccount = driver.VirtualAccount
            };
        }

        public static WithdrawnMoneyDto ToWithdrawnMoneyDto(this Driver driver)
        {
            return new WithdrawnMoneyDto
            {
                TotalMoney = driver.VirtualAccount
            };
        }
    }
}
