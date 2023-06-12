namespace MeLevaAi.Api.Domain
{
    public class Vehicle
    {
        public Vehicle(string plate, string brand, string model, int year, string color, string picture, 
            int numberOfSeats, Guid driverId, Category category)
        {
            Plate = plate;
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            DriverId = driverId;
            Category = category;
            Picture = picture;
            NumberOfSeats = numberOfSeats;
        }

        public Guid Id { get; init; } = Guid.NewGuid();
        public string Plate { get; private set; }
        public string Brand { get; private set; }   
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string Color { get; private set; }
        public string Picture { get; private set; }
        public int NumberOfSeats { get; private set; }
        public Guid DriverId { get; private set; }   
        public Category Category { get; private set; }

        public Vehicle Alterar(Vehicle veiculo)
        {
            Plate = veiculo.Plate;
            DriverId = veiculo.DriverId;
            Model = veiculo.Model;
            Color = veiculo.Color;
            Picture = veiculo.Picture;
            Category = veiculo.Category;

            return this;
        }
    }
}
