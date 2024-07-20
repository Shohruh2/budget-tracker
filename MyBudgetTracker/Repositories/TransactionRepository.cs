using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using MyBudgetTracker.Models;

namespace MyBudgetTracker.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApiDbContext _dbContext;

    public TransactionRepository(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateAsync(Transaction transaction, CancellationToken token = default)
    {
        await _dbContext.Transactions.AddAsync(transaction, token);
        var rowsAffected = await _dbContext.SaveChangesAsync(token);
        return rowsAffected > 0;
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync(Guid userId, CancellationToken token = default)
    {
        var transactions = await _dbContext.Transactions
            .Where(t => t.UserId == userId)
            .ToListAsync(token);

        return transactions;
    }

    public async Task<Transaction?> GetAsync(Guid id, CancellationToken token = default)
    {
        var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id, token);
        if (transaction == null)
        {
            return null;
        }

        return transaction;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        var transactionToDelete = await _dbContext.Transactions.FindAsync(id, token);
        if (transactionToDelete == null || transactionToDelete.UserId != userId)
        {
            return false;
        }

        _dbContext.Transactions.Remove(transactionToDelete);
        var rowsAffected = await _dbContext.SaveChangesAsync(token);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateAsync(Transaction transaction, CancellationToken token = default)
    {
        _dbContext.Transactions.Update(transaction);
        var rowsAffected = await _dbContext.SaveChangesAsync(token);
        return rowsAffected > 0;
    }
}