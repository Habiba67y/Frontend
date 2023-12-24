using ToDoApp.Domain.Common.Entities;

namespace ToDoApp.Domain.Entities;

public class ToDoItem : AuditableEntity
{
    public string Title { get; set; } = default!;

    public bool IsDone { get; set; }

    public bool IsFavorite { get; set; }

    public DateTimeOffset DueTime { get; set; }

    public DateTimeOffset ReminderTime { get; set; }
}
