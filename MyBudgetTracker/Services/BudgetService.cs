using MyBudgetTracker.Mapping;
using MyBudgetTracker.Repositories;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;

namespace MyBudgetTracker.Services;

public class BudgetService : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITransactionRepository _transactionRepository;

    public BudgetService(IBudgetRepository budgetRepository, ICategoryRepository categoryRepository, ITransactionRepository transactionRepository)
    {
        _budgetRepository = budgetRepository;
        _categoryRepository = categoryRepository;
        _transactionRepository = transactionRepository;
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

    public async Task<BudgetStatisticsResponse?> GetBudgetStatisticsAsync(Guid userId, CancellationToken token)
    {
        var budget = await _budgetRepository.GetByUserIdAsync(userId, token);
        if (budget == null) return null;

        var categories = await _categoryRepository.GetAllAsync(budget.UserId, token);

        var transactions = await _transactionRepository.GetTransactionsByUserIdAsync(userId, token);

        var statistics = new List<CategoryBudgetStatistic>();

        foreach (var category in categories)
        {
            var categoryTransactions = transactions.Where(t => t.CategoryId == category.Id);
            var totalSpent = categoryTransactions.Sum(t => t.Amount);

            statistics.Add(new CategoryBudgetStatistic
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                BudgetAmount = budget.Amount,
                TotalSpent = totalSpent,
                RemainingAmount = budget.Amount - totalSpent
            });
        }

        return new BudgetStatisticsResponse { Statistics = statistics };
    }
}