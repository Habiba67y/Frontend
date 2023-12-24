using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.Models.Dtos;
using ToDoApp.Application.ToDos.Services;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ToDoController(
    IToDoService toDoService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        var result = await toDoService.GetAsync();
        return result.Any() ? Ok(mapper.Map<IEnumerable<ToDoDto>>(result)) : NoContent();
    }

    [HttpGet("{todoId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid todoId)
    {
        var result = await toDoService.GetByIdAsync(todoId);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] ToDoDto toDoDto)
    {
        var result = await toDoService.CreateAsync(mapper.Map<ToDoItem>(toDoDto));
        return CreatedAtAction(nameof(GetById), new { ToDoId = result.Id}, result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] ToDoDto toDoDto)
    {
        var result = await toDoService.CreateAsync(mapper.Map<ToDoItem>(toDoDto));
        return Ok(result);
    }

    [HttpDelete("{todoId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid todoId)
    {
        await toDoService.DeleteByIdAsync(todoId);
        return Ok();
    }
}
