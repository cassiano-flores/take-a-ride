using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Repositories
{
    public class VehicleRepository
    {
        private static readonly List<Vehicle> _vehicles = new();

        public IEnumerable<Vehicle> List()
            => _vehicles;

        public Vehicle? Get(Guid id) 
            => (from v in _vehicles where v.Id == id select v).FirstOrDefault();

        public Vehicle Register(Vehicle vehicle)
        {
            _vehicles.Add(vehicle);

            return vehicle;
        }

        public bool Remove(Guid id)
        {
            var vehicle = Get(id);

            if(vehicle is null)
            {
                return false;
            }

            return _vehicles.Remove(vehicle);
        }
    }
}
