namespace MyBudgetTracker.Models;

public class Transactions
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid CategoryId { get; set; }

    public decimal Amount { get; set; }

    public MyEnum Type { get; set; }

    public DateTime Data { get; set; }
    
    public enum MyEnum
    {
        Income,
        Expense
    }
}