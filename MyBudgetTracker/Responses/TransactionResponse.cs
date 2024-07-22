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