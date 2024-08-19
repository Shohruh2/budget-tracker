using MyBudgetTracker.Models;

namespace MyBudgetTracker.Requests;

public class CreateCategoryDto
{
    public string Name { get; set; }

    public CategoryKind Kind { get; set; }
}