using MyBudgetTracker.Models;
using MyBudgetTracker.Requests;

namespace MyBudgetTracker.Services;

public interface ICategoryService
{
    Task<Category> CreateAsync(CreateCategoryDto request, Guid userId, CancellationToken token = default);

    Task<IEnumerable<Category>> GetAllAsync(Guid userId, CancellationToken token = default);

    Task<Category?> GetAsync(Guid categoryId, Guid userId, CancellationToken token = default);

    Task<Category?> UpdateAsync(Category category, CancellationToken token = default);

    Task<bool> DeleteAsync(Guid categoryId, Guid userId, CancellationToken token = default);
}