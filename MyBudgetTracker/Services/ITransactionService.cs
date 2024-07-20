using MyBudgetTracker.Models;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;

namespace MyBudgetTracker.Services;

public interface ITransactionService
{
    Task<TransactionResponse?> CreateAsync(CreateTransactionRequest request, Guid userId, CancellationToken token = default);

    Task<TransactionsResponse> GetAllAsync(Guid userId, CancellationToken token = default);

    Task<TransactionResponse?> GetAsync(Guid transactionId, Guid userId, CancellationToken token = default);

    Task<TransactionResponse?> UpdateAsync(Guid transactionId, UpdateTransactionRequest request, Guid userId, CancellationToken token = default);

    Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken token = default);
}