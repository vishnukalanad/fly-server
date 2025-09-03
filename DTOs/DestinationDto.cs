namespace fly_server.DTOs;

public partial class DestinationDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public decimal Price { get; set; } = 0;
    public string Image { get; set; } = "";
    public int AnnualVisitors { get; set; } = 0;
    public string Tags { get; set; } = "";
}