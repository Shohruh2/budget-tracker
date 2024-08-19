using FluentValidation;
using MyBudgetTracker.Requests;

namespace MyBudgetTracker.Validators;

public class CategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Kind)
            .NotEmpty();
        
    }
}