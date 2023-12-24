namespace AirBnB.Api.Locations.Models;

public class LocationDto
{
    public Guid? Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; }
    public int BuiltYear { get; set; }
    public int PricePerNight { get; set; }
}
