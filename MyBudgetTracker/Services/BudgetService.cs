using MyBudgetTracker.Mapping;
using MyBudgetTracker.Repositories;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;

namespace MyBudgetTracker.Services;

public class BudgetService : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly ICategoryRepository _categoryRepository;

    public BudgetService(IBudgetRepository budgetRepository, ICategoryRepository categoryRepository)
    {
        _budgetRepository = budgetRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<BudgetResponse?> CreateAsync(CreateBudgetRequest request, Guid userId, CancellationToken token = default)
    {
        var budget = request.MapToBudget(userId);

        var category = await _categoryRepository.GetAsync(request.CategoryId, token);
        if (category == null || category.UserId != userId)
        {
            return null;
        }
        
        await _budgetRepository.CreateAsync(budget, token);
        return budget.MapToResponse(category);
    }

    public async Task<BudgetResponse?> GetAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        var budget = await _budgetRepository.GetAsync(id, userId, token);
        if (budget == null)
        {
            return null;
        }

        var category = await _categoryRepository.GetAsync(budget.CategoryId, token);
        if (category == null || category.UserId != userId)
        {
            return null;
        }

        return budget.MapToResponse(category);
    }

    public async Task<BudgetsResponse?> GetAllGroupedAsync(Guid userId, CancellationToken token = default)
    {
        var budgets = await _budgetRepository.GetAllGroupedAsync(userId, token);
        return budgets!.MapToResponse();
    }

    public async Task<BudgetResponse?> UpdateAsync(UpdateBudgetRequest request, Guid id, Guid userId, CancellationToken token = default)
    {
        var budget = await _budgetRepository.GetAsync(id, userId, token);
        if (budget == null)
        {
            return null;
        }

        budget.Amount = request.Amount;
        budget.Period = request.Period;

        await _budgetRepository.UpdateAsync(budget, token);

        var category = await _categoryRepository.GetAsync(budget.CategoryId, token);
        if (category == null || category.UserId != userId)
        {
            return null;
        }

        return budget.MapToResponse(category);
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        return await _budgetRepository.DeleteAsync(id, userId, token);
    }
}