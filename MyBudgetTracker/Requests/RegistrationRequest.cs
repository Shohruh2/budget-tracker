namespace MyBudgetTracker.Requests;

public class RegistrationRequest
{
    public string Name { get; set; }
    
    public required string Email { get; init; }
    
    public string Gender { get; set; }
    
    public DateTime Birthday { get; set; }

    public string Password { get; set; }
}