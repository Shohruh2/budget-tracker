using MyBudgetTracker.Models;

namespace MyBudgetTracker.Requests;

public class UpdateBudgetRequest
{
    public decimal Amount { get; set; }

    public BudgetPeriod Period { get; set; }
}