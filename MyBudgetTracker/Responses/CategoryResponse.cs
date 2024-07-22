using MyBudgetTracker.Models;

namespace MyBudgetTracker.Responses;

public class CategoryResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public CategoryKind Kind { get; set; }

    public DateTime CreatedDate { get; set; }
}