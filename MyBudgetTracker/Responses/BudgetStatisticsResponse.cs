namespace MyBudgetTracker.Responses;

public class BudgetStatisticsResponse
{
    public List<CategoryBudgetStatistic> Statistics { get; init; }
}

public class CategoryBudgetStatistic
{
    public Guid CategoryId { get; set; }
    public required string CategoryName { get; set; }
    public decimal BudgetAmount { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal RemainingAmount { get; set; }
}