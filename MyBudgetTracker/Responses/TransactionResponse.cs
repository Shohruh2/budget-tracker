using MyBudgetTracker.Models;

namespace MyBudgetTracker.Responses;

public class TransactionResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; }

    public ResponseCategory Category { get; set; }
}

public class ResponseCategory
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public CategoryKind Type { get; set; }
}