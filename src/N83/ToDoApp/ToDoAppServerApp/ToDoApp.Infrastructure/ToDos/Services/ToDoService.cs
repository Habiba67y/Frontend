using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoApp.Application.ToDos.Services;
using ToDoApp.Domain.Entities;
using ToDoApp.Persistence.Repositories.Interfaces;

namespace ToDoApp.Infrastructure.ToDos.Services;

public class ToDoService(IToDoRepository toDoRepository, IValidator<ToDoItem> todoValidator) : IToDoService
{
    public IQueryable<ToDoItem> Get(
        Expression<Func<ToDoItem, bool>>? predicate = default, 
        bool asNoTracking = false) =>
    toDoRepository.Get(predicate, asNoTracking);

    public async ValueTask<IList<ToDoItem>> GetAsync(bool asNoTracking = false)
    {
        var todos = await Get().ToListAsync();

        return todos
            .Where(todo => !todo.IsDone && todo.DueTime > DateTime.Now).OrderBy(todo => todo.DueTime) //active todos
            .Concat(todos.Where(todo => todo.IsDone).OrderByDescending(todo => todo.ModifiedTime)) //completed todos
            .Concat(todos.Where(todo => !todo.IsDone && todo.DueTime <= DateTime.Now).OrderByDescending(todo => todo.DueTime)) //overdue todos
            .ToList();
    }

    public ValueTask<ToDoItem?> GetByIdAsync(
        Guid id, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default) =>
    toDoRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<IList<ToDoItem>> GetByIdsAsync(
        IEnumerable<Guid> ids, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default) =>
    toDoRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public ValueTask<ToDoItem> CreateAsync(
        ToDoItem toDoItem, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = todoValidator.Validate(toDoItem);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        toDoItem.CreatedTime = DateTimeOffset.UtcNow;

        return toDoRepository.CreateAsync(toDoItem, saveChanges, cancellationToken);      
    }

    public ValueTask<ToDoItem> UpdateAsync(
        ToDoItem toDoItem, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = todoValidator.Validate(toDoItem);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return toDoRepository.UpdateAsync(toDoItem, saveChanges, cancellationToken);
    }

    public ValueTask<ToDoItem?> DeleteAsync(
        ToDoItem toDoItem,
        bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
    toDoRepository.DeleteAsync(toDoItem, saveChanges, cancellationToken);

    public ValueTask<ToDoItem?> DeleteByIdAsync(
        Guid id, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    toDoRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);
}   

