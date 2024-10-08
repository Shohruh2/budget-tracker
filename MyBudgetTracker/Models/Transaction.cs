﻿namespace MyBudgetTracker.Models;

public class Transaction
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid CategoryId { get; set; }

    public CategoryKind Type { get; set; }
    
    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; }
    
    public virtual Category Category { get; set; }
}