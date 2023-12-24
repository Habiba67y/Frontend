using AutoMapper;
using ToDoApp.Api.Models.Dtos;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Api.Mappers;

public class ToDoMapper : Profile
{
    public ToDoMapper()
    {
        CreateMap<ToDoDto, ToDoItem>().ReverseMap();
    }
}
