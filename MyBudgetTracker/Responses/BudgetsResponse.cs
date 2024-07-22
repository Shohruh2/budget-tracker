namespace MyBudgetTracker.Responses;

public class BudgetsResponse
{
    public IEnumerable<BudgetResponse> Items { get; set; } = Enumerable.Empty<BudgetResponse>();
}