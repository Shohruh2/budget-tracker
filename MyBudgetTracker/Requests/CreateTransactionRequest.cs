namespace MyBudgetTracker.Requests;

public class CreateTransactionRequest
{
    public Guid CategoryId { get; set; }
    
    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; }
}