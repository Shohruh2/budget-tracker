using MyBudgetTracker.Models;
using MyBudgetTracker.Repositories;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;

namespace MyBudgetTracker.Mapping;

public static class ContractMapping
{
    public static User MapToUser(this CreateUserRequest request)
    {
        return new User
        {
            Id = request.Id, // id in database
            Name = request.Name,
            Gender = request.Gender,
            Birthday = request.Birthday
        };
    }

    public static Category MapToCategory(this CreateCategoryDto request, Guid userId)
    {
        return new Category()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Type = request.Type,
            CreatedDate = DateTime.Now,
            UserId = userId
        };
    }

    public static CategoryResponse MapToResponse(this Category category)
    {
        return new CategoryResponse()
        {
            Id = category.Id,
            Name = category.Name,
            UserId = category.UserId,
            CreatedDate = category.CreatedDate,
            Type = category.Type
        };
    }
    
    public static CategoriesResponse MapToResponse(this IEnumerable<Category> categories)
    {
        return new CategoriesResponse
        {
            Items = categories.Select(MapToResponse)
        };
    }
    
    public static Category MapToCategory(this UpdateCategoryRequest request, Guid id, Guid userId)
    {
        return new Category
        {
            Id = id,
            UserId = userId,
            Name = request.Name,
            Type = request.Type,
        };
    }
}