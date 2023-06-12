using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Repositories;

public class RideRepository
{
    private static readonly List<Ride> _rides = new();
    
    public IEnumerable<Ride> List() => _rides;

    public Ride RequestRide(Ride ride)
    {
        ride.Id = _rides.Count + 1;
        ride.SetSolicitedRide(ride);
        _rides.Add(ride);

        return ride;
    }

    public Ride? Get(int id)
        => (from r in _rides where r.Id == id select r).FirstOrDefault();
}
