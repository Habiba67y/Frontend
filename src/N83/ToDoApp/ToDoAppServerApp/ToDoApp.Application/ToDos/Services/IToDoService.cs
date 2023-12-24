using System.Linq.Expressions;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDos.Services;

public interface IToDoService
{
    IQueryable<ToDoItem> Get(
        Expression<Func<ToDoItem, bool>>? predicate = default,
        bool asNoTracking = false);

    ValueTask<IList<ToDoItem>> GetAsync(bool asNoTracking = false);

    ValueTask<ToDoItem?> GetByIdAsync(
        Guid id,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<IList<ToDoItem>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<ToDoItem> CreateAsync(
        ToDoItem toDoItem,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );

    ValueTask<ToDoItem> UpdateAsync(
        ToDoItem toDoItem,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );

    ValueTask<ToDoItem?> DeleteAsync(
        ToDoItem toDoItem,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );

    ValueTask<ToDoItem?> DeleteByIdAsync(
        Guid id,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );
}
