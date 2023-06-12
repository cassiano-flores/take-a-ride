namespace MeLevaAi.Api.Domain
{
    public class Passenger
    {
        public Passenger(string name, string cpf, string email, DateOnly birthDate)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            BirthDate = birthDate;
        }

        public Guid Id { get; init; } = Guid.NewGuid();
        public double VirtualAccount { get; private set; }
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }   
        public int PassengerRating { get; private set; }
        public DateOnly BirthDate { get; private set; }

        public Passenger Alterar(Passenger passageiro)
        {
            VirtualAccount = passageiro.VirtualAccount;
            Name = passageiro.Name;
            Cpf = passageiro.Cpf;
            PassengerRating = passageiro.PassengerRating;
            BirthDate = passageiro.BirthDate;

            return this;
        }

        public void UpdateVirtualAccount(double newAmount)
        {
            VirtualAccount = newAmount;
        }
    }
}
