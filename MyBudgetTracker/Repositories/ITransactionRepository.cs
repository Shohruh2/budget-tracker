﻿using MyBudgetTracker.Models;

namespace MyBudgetTracker.Repositories;

public interface ITransactionRepository
{
    Task<bool> CreateAsync(Transaction transaction, CancellationToken token = default);

    Task<List<Transaction>> GetAllAsync(Guid userId, CancellationToken token = default);

    Task<List<IGrouping<Category, Transaction>>?> GetAllGroupedAsync(Guid userId, CancellationToken token = default);

    Task<Transaction?> GetAsync(Guid id, CancellationToken token = default);

    Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken token = default);

    Task<bool> UpdateAsync(Transaction transaction, CancellationToken token = default);

    Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(Guid userId, CancellationToken token);
}