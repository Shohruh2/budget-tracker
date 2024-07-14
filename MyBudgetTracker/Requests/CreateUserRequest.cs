namespace MyBudgetTracker.Requests;

public class CreateUserRequest
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Gender { get; set; }
    
    public DateTime Birthday { get; set; }
}