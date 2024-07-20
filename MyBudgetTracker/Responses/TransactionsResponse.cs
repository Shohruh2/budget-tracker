namespace MyBudgetTracker.Responses;

public class TransactionsResponse
{
    public required IEnumerable<TransactionResponse> Items { get; init; } = Enumerable.Empty<TransactionResponse>();
}