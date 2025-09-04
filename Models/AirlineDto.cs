namespace fly_server.Models;

public partial class AirlineDto
{
    public int? Id { get; set; }
    public string AirlineName { get; set; } = "";
    public string Country { get; set; } = "";
    public string Code { get; set; } = "";
    public int Rating { get; set; }
    public string Logo { get; set; } = "";
    public string Website { get; set; } = "";
}