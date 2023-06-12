namespace MeLevaAi.Api.Contracts.Responses.Passenger
{
    public class PassengerDto
    {
        public Guid Id { get; set; }
        public double VirtualAccount { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public int PassengerRating { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
