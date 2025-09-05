namespace fly_server.Models;

public partial class HotelDto
{
    public HotelDto()
    {
    }

    public int? Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Latitude { get; set; } = "";
    public string Longitude { get; set; } = "";
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public int Rating { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; } = "";
    public List<AmenityDto> Amenities { get; set; } = new List<AmenityDto>();
}