using MyBudgetTracker.Models;

namespace MyBudgetTracker.Requests;

public class CreateBudgetRequest
{
    public Guid CategoryId { get; set; }

    public decimal Amount { get; set; }

    public BudgetPeriod Period { get; set; }
}