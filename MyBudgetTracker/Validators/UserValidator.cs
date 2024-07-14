using FluentValidation;
using MyBudgetTracker.Requests;

namespace MyBudgetTracker.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Gender)
            .NotEmpty();
        RuleFor(x => x.Birthday)
            .NotEmpty();
    }
}