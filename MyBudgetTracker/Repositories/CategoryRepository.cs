using Microsoft.EntityFrameworkCore;
using MyBudgetTracker.Models;

namespace MyBudgetTracker.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApiDbContext _dbContext;

    public CategoryRepository(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateAsync(Category category, CancellationToken token = default)
    {
        await _dbContext.Categories.AddAsync(category, token);
        var rowsAffected = await _dbContext.SaveChangesAsync(token);
        return rowsAffected > 0;
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken token = default)
    {
        var categories = await _dbContext.Categories.ToListAsync(token);
        return categories;
    }

    public async Task<Category?> GetAsync(Guid id, CancellationToken token = default)
    {
        var category = await _dbContext.Categories.FindAsync(id, token);
        if (category == null)
        {
            return null;
        }
        
        return category;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
    {
        var categoryToDelete = await _dbContext.Categories.FindAsync(id, token);
        if (categoryToDelete == null)
        {
            return false;
        }

        _dbContext.Categories.Remove(categoryToDelete);
        var rowsAffected = await _dbContext.SaveChangesAsync(token);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateAsync(Category category, CancellationToken token = default)
    {
        var categoryToUpdate = await _dbContext.Categories.FindAsync(category.Id, token);
        if (categoryToUpdate == null)
        {
            return false;
        }

        categoryToUpdate.Name = category.Name;
        categoryToUpdate.Type = category.Type;

        var rowsAffected = await _dbContext.SaveChangesAsync(token);
        return rowsAffected > 0;
    }
}