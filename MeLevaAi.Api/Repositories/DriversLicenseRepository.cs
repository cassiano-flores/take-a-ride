using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Repositories;

public class DriversLicenseRepository
{
    private static readonly List<DriversLicense> _driversLicenses = new();

    public IEnumerable<DriversLicense> List()
        => _driversLicenses;

    public DriversLicense? Get(Guid id)
        => (from ds in _driversLicenses where ds.Id == id select ds).FirstOrDefault();

    public DriversLicense Register(DriversLicense driversLicense)
    {
        _driversLicenses.Add(driversLicense);

        return driversLicense;
    }

    public bool Remove(Guid id)
    {
        var driversLicense = Get(id);

        if (driversLicense == null)
            return false;

        return _driversLicenses.Remove(driversLicense);
    }
}