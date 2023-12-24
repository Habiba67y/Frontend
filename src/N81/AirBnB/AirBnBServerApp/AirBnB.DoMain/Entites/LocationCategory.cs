using AirBnB.DoMain.Common.Entities;

namespace AirBnB.DoMain.Entites;

public class LocationCategory : IEntity
{
    public Guid Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; }
}
