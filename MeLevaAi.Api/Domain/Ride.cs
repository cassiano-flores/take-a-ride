namespace MeLevaAi.Api.Domain;

public class Ride
{
    const int ExponentTwo = 2;
    const int solicitedRideStatus = 0;
    const int initiatedRideStatus = 1;
    const int finishedRideStatus = 2;

    public Ride(Guid passengerId, double initialPointX, double initialPointY, double finalPointX, double finalPointY)
    {
        PassengerId = passengerId;
        InitialPointX = initialPointX;
        InitialPointY = initialPointY;
        FinalPointX = finalPointX;
        FinalPointY = finalPointY;
    }

    public int Id { get; set; }
    public Guid PassengerId { get; private set; }
    public Guid VehicleId { get; set; }
    public Guid DriverId { get; set; }
    public RideStatus RideStatus { get; private set; }
    public DateTime StartedRide { get; private set; }   
    public DateTime FinishedRide { get; private set; }  
    public double InitialPointX { get; private set; }
    public double InitialPointY { get; private set; }
    public double FinalPointX { get; private set; }
    public double FinalPointY { get; private set; }
    public int EstimatedArrivalTime { get; private set; }
    public double TotalEstimatedRideTime { get; private set; }
    public double TotalEstimatedRideValue { get; private set; }

    public void SetSolicitedRide(Ride ride)
    {
        ride.RideStatus = solicitedRideStatus;
    }

    public void SetEstimatedArrivalTime(int value)
    {
        EstimatedArrivalTime = value;
    }

    public void SetInitiatedRide(Ride ride)
    {
        ride.RideStatus = (RideStatus)initiatedRideStatus;
    }

    public void SetFinishedRide(Ride ride)
    {
        ride.RideStatus = (RideStatus)finishedRideStatus;
    }

    public double CalculateRideDistance(Ride ride)
    {
        return Math.Sqrt(Math.Pow(ride.FinalPointX - ride.InitialPointX, ExponentTwo) + 
            Math.Pow(ride.FinalPointY - ride.InitialPointY, ExponentTwo));
    }

    public void SetTimeAndRideValue(Ride ride, double totalEstimatedRideTime, double totalEstimatedRideValue)
    {
        ride.TotalEstimatedRideTime = totalEstimatedRideTime;
        ride.TotalEstimatedRideValue = totalEstimatedRideValue;
    }

    public void SetStartedRideDate(Ride ride)
    {
        ride.StartedRide = DateTime.Now;
    }

    public void SetFinishedRideDate(Ride ride)
    {
        ride.FinishedRide = DateTime.Now;   
    }
}
