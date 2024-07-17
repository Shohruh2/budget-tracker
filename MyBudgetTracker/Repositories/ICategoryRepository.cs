using MyBudgetTracker.Models;

namespace MyBudgetTracker.Repositories;

public interface ICategoryRepository
{
    Task<bool> CreateAsync(Category category, CancellationToken token = default);

    Task<IEnumerable<Category>> GetAllAsync(Guid userId, CancellationToken token = default);
    
    Task<Category?> GetAsync(Guid categoryId, CancellationToken token = default);

    Task<bool> DeleteAsync(Guid categoryId, Guid userId, CancellationToken token = default);

    Task<bool> UpdateAsync(Category category, CancellationToken token = default);
}