using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using MyBudgetTracker.Models;

namespace MyBudgetTracker.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly ApiDbContext _dbContext;

    public BudgetRepository(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateAsync(Budget budget, CancellationToken token = default)
    {
        await _dbContext.Budgets.AddAsync(budget, token);

        var rowsAffected = await _dbContext.SaveChangesAsync(token);
        return rowsAffected > 0;
    }

    public async Task<Budget?> GetAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        var budget = await _dbContext.Budgets.FirstOrDefaultAsync(b => b.Id == id, token);
        if (budget == null || budget.UserId != userId)
        {
            return null;
        }

        return budget;
    }

    public async Task<List<IGrouping<Category, Budget>>?> GetAllGroupedAsync(Guid userId, CancellationToken token = default)
    {
        var budgets = await _dbContext.Budgets
            .Where(b => b.UserId == userId)
            .Include(x => x.Category)
            .GroupBy(x => x.Category)
            .ToListAsync(token);

        return budgets;
    }

    public async Task<bool> UpdateAsync(Budget budget, CancellationToken token = default)
    {
        _dbContext.Budgets.Update(budget);
        var rowsAffected = await _dbContext.SaveChangesAsync(token);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        var budgetToDelete = await _dbContext.Budgets.FindAsync(id, token);
        if (budgetToDelete == null || budgetToDelete.UserId != userId)
        {
            return false;
        }

        _dbContext.Budgets.Remove(budgetToDelete);
        var rowsAffected = await _dbContext.SaveChangesAsync(token);
        return rowsAffected > 0;
    }
}