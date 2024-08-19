using MyBudgetTracker.Models;

namespace MyBudgetTracker.Responses;

public class ResponseCategory
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public CategoryKind Kind { get; set; }
}