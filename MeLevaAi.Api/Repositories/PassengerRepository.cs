using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Repositories
{
    public class PassengerRepository
    {
        private static readonly List<Passenger> _passengers = new();

        public IEnumerable<Passenger> List() => _passengers;

        public Passenger? Get(Guid id) 
            => (from p in _passengers where p.Id == id select p).FirstOrDefault();

        public Passenger Register(Passenger passenger)
        {
            _passengers.Add(passenger);

            return passenger;  
        }

        public bool Remove(Guid id)
        {
            var passenger = Get(id);

            if(passenger is null)
            {
                return false;
            }

            return _passengers.Remove(passenger);
        }
    }
}
