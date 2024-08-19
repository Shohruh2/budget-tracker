using FluentValidation;
using MyBudgetTracker.Requests;

namespace MyBudgetTracker.Validators;

public class TransactionValidator : AbstractValidator<CreateTransactionRequest>
{
    public TransactionValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotNull();
        RuleFor(x => x.Amount)
            .NotNull();
        RuleFor(x => x.Date)
            .NotNull();
        RuleFor(x => x.Description)
            .NotNull();
    }
}