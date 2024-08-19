using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;

namespace MyBudgetTracker.Services;

public interface IAuthService
{
    Task<bool> Registration(RegistrationRequest request, CancellationToken token = default);

    Task<bool> ConfirmRegistration(ConfirmRegistrationRequest request, CancellationToken token = default);

    Task<AuthLoginResponse?> Login(LoginRequest loginRequest, CancellationToken token = default);
}