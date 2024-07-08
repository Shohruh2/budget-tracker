namespace MyBudgetTracker.Models;

public class Budget
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public decimal TotalIncome { get; set; }

    public decimal TotalExpense { get; set; }

    public DateTime CreatedAt { get; set; }
}