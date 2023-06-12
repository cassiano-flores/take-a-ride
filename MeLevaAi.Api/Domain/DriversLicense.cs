namespace MeLevaAi.Api.Domain;

public class DriversLicense
{
    public DriversLicense(string number, Category category, DateOnly expiration)
    {
        Number = number;
        Category = category;
        Expiration = expiration;
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public string Number { get; private set; }
    public Category Category { get; private set; }
    public DateOnly Expiration { get; private set; }
}
