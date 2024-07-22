using Microsoft.EntityFrameworkCore;
using MyBudgetTracker.Models;

namespace MyBudgetTracker;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Budget>(entity =>
        {
            entity.Property(b => b.Amount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(t => t.Amount).HasColumnType("decimal(18, 2)");
        });
    }
    
    public DbSet<User> Users { get; init; }
    
    public DbSet<Category> Categories { get; init; }
    
    public DbSet<Transaction> Transactions { get; init; }
    
    public DbSet<Budget> Budgets { get; init; }
}