namespace MyBudgetTracker.Requests;

public class ConfirmRegistrationRequest
{
    public string Username { get; set; }

    public string ConfrimationCode { get; set; }
}