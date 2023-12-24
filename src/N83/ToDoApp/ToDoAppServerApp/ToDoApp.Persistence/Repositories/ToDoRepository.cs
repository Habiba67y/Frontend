using System.Linq.Expressions;
using ToDoApp.Domain.Entities;
using ToDoApp.Persistence.DataContexts;
using ToDoApp.Persistence.Repositories.Interfaces;

namespace ToDoApp.Persistence.Repositories;

public class ToDoRepository : EntityRepositoryBase<ToDoItem, AppDbContext>, IToDoRepository
{
    public ToDoRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<ToDoItem> Get(
        Expression<Func<ToDoItem, bool>>? predicate = null, 
        bool asNoTracking = false) =>
    base.Get(predicate, asNoTracking);

    public ValueTask<ToDoItem?> GetByIdAsync(
        Guid id, bool asNoTracking = false, 
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<IList<ToDoItem>> GetByIdsAsync(
        IEnumerable<Guid> ids, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default) =>
    base.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public ValueTask<ToDoItem> CreateAsync(
        ToDoItem toDoItem, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(toDoItem, saveChanges, cancellationToken);

    public ValueTask<ToDoItem> UpdateAsync(
        ToDoItem toDoItem,
        bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
    base.UpdateAsync(toDoItem, saveChanges, cancellationToken);

    public ValueTask<ToDoItem?> DeleteAsync(
        ToDoItem toDoItem,
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    base.DeleteAsync(toDoItem, saveChanges, cancellationToken);

    public ValueTask<ToDoItem?> DeleteByIdAsync(
        Guid id, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) =>
    base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}
