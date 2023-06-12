namespace MeLevaAi.Api.Contracts.Responses.Driver
{
    public class DriverDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Cpf { get; set; }
        public Guid LicenseId { get; set; }
        public double VirtualAccount { get; set; }
    }
}
