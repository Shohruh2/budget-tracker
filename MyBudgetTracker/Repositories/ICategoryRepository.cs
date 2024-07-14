using MyBudgetTracker.Models;

namespace MyBudgetTracker.Repositories;

public interface ICategoryRepository
{
    Task<bool> CreateAsync(Category category, CancellationToken token = default);

    Task<IEnumerable<Category>> GetAllAsync(CancellationToken token = default);
    
    Task<Category?> GetAsync(Guid id, CancellationToken token = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken token = default);

    Task<bool> UpdateAsync(Category category, CancellationToken token = default);
}