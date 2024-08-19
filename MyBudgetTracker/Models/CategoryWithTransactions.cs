namespace MyBudgetTracker.Models;

public class CategoryWithTransactions
{
    public Category Category { get; set; }

    public List<Transaction> Transactions { get; set; }
}