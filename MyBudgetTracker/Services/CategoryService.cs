using FluentValidation;
using MyBudgetTracker.Mapping;
using MyBudgetTracker.Models;
using MyBudgetTracker.Repositories;
using MyBudgetTracker.Requests;

namespace MyBudgetTracker.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<CreateCategoryDto> _validator;

    public CategoryService(ICategoryRepository categoryRepository, IValidator<CreateCategoryDto> validator)
    {
        _categoryRepository = categoryRepository;
        _validator = validator;
    }

    public async Task<Category> CreateAsync(CreateCategoryDto request, Guid userId, CancellationToken token = default)
    {
        await _validator.ValidateAndThrowAsync(request, token);
        var category = request.MapToCategory(userId);
        await _categoryRepository.CreateAsync(category, token);
        return category;
    }

    public async Task<List<Category>> GetAllAsync(Guid userId, CancellationToken token = default)
    {
        var categories = await _categoryRepository.GetAllAsync(userId, token);
        return categories;
    }

    public async Task<Category?> GetAsync(Guid categoryId, Guid userId, CancellationToken token = default)
    {
        var category = await _categoryRepository.GetAsync(categoryId, token);

        if (category != null && category.UserId != userId)
        {
            return null;
        }
        
        return category;
    }

    public async Task<Category> UpdateAsync(Category category, CancellationToken token = default)
    {
        await _categoryRepository.UpdateAsync(category, token);
        return category;
    }

    public async Task<bool> DeleteAsync(Guid categoryId, Guid userId, CancellationToken token = default)
    {
        return await _categoryRepository.DeleteAsync(categoryId, userId, token);
    }
}