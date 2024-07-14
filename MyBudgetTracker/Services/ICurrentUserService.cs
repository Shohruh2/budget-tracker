using MyBudgetTracker.Models;

namespace MyBudgetTracker.Services;

public interface ICurrentUserService
{
    CurrentUser GetCurrentUser();
}