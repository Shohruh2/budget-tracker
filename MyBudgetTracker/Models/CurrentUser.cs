namespace MyBudgetTracker.Models;

public class CurrentUser
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public string Gender { get; set; }
    
    public DateTime Birthday { get; set; }
}