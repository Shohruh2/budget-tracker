using MyBudgetTracker.Models;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;

namespace MyBudgetTracker.Services;

public interface IBudgetService
{
    Task<BudgetResponse?> CreateAsync(CreateBudgetRequest request, Guid userId, CancellationToken token = default);

    Task<BudgetResponse?> GetAsync(Guid id, Guid userId, CancellationToken token = default);

    Task<BudgetsResponse?> GetAllGroupedAsync(Guid userId, CancellationToken token = default);

    Task<BudgetResponse?> UpdateAsync(UpdateBudgetRequest request, Guid id, Guid userId, CancellationToken token = default);

    Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken token = default);

    Task<BudgetStatisticsResponse?> GetBudgetStatisticsAsync(Guid userId, CancellationToken token = default);
}