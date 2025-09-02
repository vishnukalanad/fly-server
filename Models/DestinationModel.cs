namespace fly_server.Models;

public partial class DestinationModel
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public decimal Price { get; set; } = 0;
    public string Image { get; set; } = "";
    public int AnnualVisits { get; set; } = 0;
    public List<string> Tags { get; set; } = [];
}