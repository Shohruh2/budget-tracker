namespace MyBudgetTracker.Models;

public class Category
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public CategoryKind Kind { get; set; }
    
    public string Name { get; set; }
    
    public DateTime CreatedDate { get; set; }
}