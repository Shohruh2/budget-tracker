﻿namespace MyBudgetTracker.Models;

public class Budget
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid CategoryId { get; set; }

    public decimal Amount { get; set; }
    
    public BudgetPeriod Period { get; set; }

    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public virtual Category Category { get; set; }
}