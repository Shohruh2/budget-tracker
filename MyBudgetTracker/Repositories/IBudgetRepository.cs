using MyBudgetTracker.Models;

namespace MyBudgetTracker.Repositories;

public interface IBudgetRepository
{
    Task<bool> CreateAsync(Budget budget, CancellationToken token = default);

    Task<Budget?> GetAsync(Guid id, Guid userId, CancellationToken token = default);

    Task<List<IGrouping<Category, Budget>>?> GetAllGroupedAsync(Guid userId, CancellationToken token = default);

    Task<bool> UpdateAsync(Budget budget,CancellationToken token = default);

    Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken token = default);

    Task<Budget?> GetByUserIdAsync(Guid userId, CancellationToken token = default);
}