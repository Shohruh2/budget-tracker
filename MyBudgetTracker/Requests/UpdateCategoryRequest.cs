using MyBudgetTracker.Models;

namespace MyBudgetTracker.Requests;

public class UpdateCategoryRequest
{
    public string Name { get; set; }

    public CategoryKind Kind { get; set; }
}