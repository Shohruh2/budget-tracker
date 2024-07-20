using Microsoft.EntityFrameworkCore;
using MyBudgetTracker.Models;

namespace MyBudgetTracker;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Transaction> Transactions { get; set; }
}