using MyBudgetTracker.Models;

namespace MyBudgetTracker.Responses;

public class BudgetResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public decimal Amount { get; set; }
    
    public BudgetPeriod Period { get; set; }

    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public virtual ResponseCategory Category { get; set; }
}