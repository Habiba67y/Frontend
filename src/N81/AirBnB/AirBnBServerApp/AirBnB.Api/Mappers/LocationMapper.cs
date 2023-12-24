using AirBnB.Api.Locations.Models;
using AirBnB.DoMain.Entites;
using AutoMapper;

namespace AirBnB.Api.Mappers;

public class LocationMapper : Profile
{
    public LocationMapper()
    {
        CreateMap<Location, LocationDto>().ReverseMap();
    }
}
