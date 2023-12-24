using AirBnB.DoMain.Common.Entities;

namespace AirBnB.DoMain.Entites;

public class Location : IEntity
{
    public Guid Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; }
    public int BuiltYear { get; set; }
    public int PricePerNight { get; set; }
}
