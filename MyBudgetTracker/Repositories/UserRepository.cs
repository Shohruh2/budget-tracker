using MyBudgetTracker.Models;

namespace MyBudgetTracker.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApiDbContext _dbContext;

    public UserRepository(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<bool> CreateAsync(User user, CancellationToken token = default)
    {
        await _dbContext.Users.AddAsync(user, token);
        var rowsAffected = await _dbContext.SaveChangesAsync(token);
        return rowsAffected > 0;
    }
}