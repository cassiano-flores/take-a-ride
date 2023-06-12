namespace MeLevaAi.Api.Domain
{
    public class Driver
    {
        public Driver(string name, string email, DateOnly birthDate, string cpf, Guid licenseId)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Cpf = cpf;
            LicenseId = licenseId;
        }

        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public double VirtualAccount { get; private set; } 
        public string Cpf { get; private set; }
        public Guid LicenseId { get; private set; }

        public void UpdateVirtualAccount(double amount)
        {
            VirtualAccount = amount;
        }
    }
}
