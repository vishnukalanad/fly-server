namespace fly_server.Models;

public class TripModel
{
    public string Departure { get; set; } = "";
    public string Arrival { get; set; } = "";
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
}