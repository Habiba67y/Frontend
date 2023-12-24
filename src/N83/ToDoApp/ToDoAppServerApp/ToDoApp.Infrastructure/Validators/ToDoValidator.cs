using FluentValidation;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Infrastructure.Validators;

public class ToDoValidator : AbstractValidator<ToDoItem>
{
    public ToDoValidator()
    {
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(todo => todo.Title).NotEmpty().MinimumLength(3).MaximumLength(128);
                RuleFor(todo => todo.IsDone).Equal(false);
                RuleFor(todo => todo.IsFavorite).Equal(false);
                RuleFor(todo => todo.DueTime).GreaterThan(DateTimeOffset.UtcNow);
                RuleFor(todo => todo.ReminderTime).GreaterThan(DateTimeOffset.UtcNow);
            }
        );

        RuleSet(
            EntityEvent.OnUpdate.ToString(),
            () =>
            {
                RuleFor(todo => todo.Title).NotEmpty().MinimumLength(3).MaximumLength(128);
                RuleFor(todo => todo.DueTime).GreaterThan(DateTimeOffset.UtcNow);
                RuleFor(todo => todo.ReminderTime).GreaterThan(DateTimeOffset.UtcNow);
            }
        );
    }
}
