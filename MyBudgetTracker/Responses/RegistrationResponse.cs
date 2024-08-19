namespace MyBudgetTracker.Responses;

public class RegistrationResponse
{
    public string Message { get; set; }
    
    public string Name { get; set; }
    
    public required string Email { get; init; }
    
    public string Gender { get; set; }
    
    public DateTime Birthday { get; set; }

    public string Password { get; set; }
}