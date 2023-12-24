using AirBnB.Api.Models.Dtos;
using AirBnB.DoMain.Entites;
using AutoMapper;

namespace AirBnB.Api.Mappers;

public class LocationCategoryMapper : Profile
{
    public LocationCategoryMapper()
    {
        CreateMap<LocationCategory, LocationCategoryDto>().ReverseMap();
    }
}
