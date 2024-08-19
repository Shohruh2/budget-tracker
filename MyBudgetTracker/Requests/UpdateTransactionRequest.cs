namespace MyBudgetTracker.Requests;

public class UpdateTransactionRequest
{
    public decimal Amount { get; set; }

    public string Description { get; set; }
}