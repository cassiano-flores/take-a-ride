using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Repositories
{
    public class DriverRepository
    {
        private static readonly List<Driver> _drivers = new();

        public IEnumerable<Driver> List()
            => _drivers;

        public Driver? Get(Guid id)
            => (from d in _drivers where d.Id == id select d).FirstOrDefault();

        public Driver Register(Driver driver)
        {
            _drivers.Add(driver);

            return driver;
        }

        public void Remove(Guid id)
        {
            var driver = Get(id);

            if (driver != null)
            {
                _drivers.Remove(driver);
            }
        }
    }
}
