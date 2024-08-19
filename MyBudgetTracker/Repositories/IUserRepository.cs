using MyBudgetTracker.Models;

namespace MyBudgetTracker.Repositories;

public interface IUserRepository
{
    Task<bool> CreateAsync(User user, CancellationToken token = default);
}