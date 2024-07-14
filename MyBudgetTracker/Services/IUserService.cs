using MyBudgetTracker.Models;
using MyBudgetTracker.Requests;

namespace MyBudgetTracker.Services;

public interface IUserService
{
    Task<User> CreateAsync(CreateUserRequest request, CancellationToken token = default);
}