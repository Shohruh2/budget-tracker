using MyBudgetTracker.Models;
using MyBudgetTracker.Repositories;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;

namespace MyBudgetTracker.Mapping;

public static class ContractMapping
{
    public static User MapToUser(this CreateUserRequest request)
    {
        return new User
        {
            Id = request.Id, // id in database
            Name = request.Name,
            Gender = request.Gender,
            Birthday = request.Birthday
        };
    }

    public static Category MapToCategory(this CreateCategoryDto request, Guid userId)
    {
        return new Category()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Kind = request.Kind,
            CreatedDate = DateTime.Now,
            UserId = userId
        };
    }

    public static CategoryResponse MapToResponse(this Category category)
    {
        return new CategoryResponse()
        {
            Id = category.Id,
            Name = category.Name,
            UserId = category.UserId,
            CreatedDate = category.CreatedDate,
            Kind = category.Kind
        };
    }
    
    public static CategoriesResponse MapToResponse(this IEnumerable<Category> categories)
    {
        return new CategoriesResponse
        {
            Items = categories.Select(MapToResponse)
        };
    }
    
    public static Category MapToCategory(this UpdateCategoryRequest request, Guid id, Guid userId, Category existingCategory)
    {
        existingCategory.Name = request.Name;
        existingCategory.Kind = request.Kind;

        return existingCategory;
    }

    public static Transaction MapToTransaction(this CreateTransactionRequest request, Guid userId)
    {
        return new Transaction()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = request.Amount,
            Date = request.Date,
            Description = request.Description,
            CategoryId = request.CategoryId
        };
    }

    public static TransactionResponse MapToResponse(this Transaction transaction, Category category)
    {
        return new TransactionResponse()
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            Category = new ResponseCategory()
            {
                Id = transaction.CategoryId,
                Name = category.Name,
                Type = category.Kind,
            },
            Amount = transaction.Amount,
            Date = transaction.Date,
            Description = transaction.Description,
        };
    }
    
    public static TransactionsResponse MapToResponse(this List<IGrouping<Category, Transaction>?> groupedTransactions)
    {
        // var result = new List<TransactionResponse>();
        // foreach (var grouping in groupedTransactions)
        // {
        //     foreach (var transaction in grouping)
        //     {
        //         result.Add(transaction.MapToResponse(grouping.Key));
        //     }
        // }
        
        // Which is same as above ^
        var result = (
            from grouping in groupedTransactions 
            from transaction in grouping 
            select transaction.MapToResponse(grouping.Key)).ToList();
        return new TransactionsResponse()
        {
            Items = result
        };
    }
    
    public static TransactionsResponse MapToResponseOld(this IEnumerable<Transaction> transactions, IDictionary<Guid, Category> categories)
    {
        return new TransactionsResponse()
        {
            Items = transactions.Select(transaction => transaction.MapToResponse(categories[transaction.CategoryId]))
        };
    }
    
    public static Transaction MapToTransaction(this UpdateTransactionRequest request, Guid id, Guid userId, Transaction existingTransaction)
    {
        existingTransaction.Amount = request.Amount;
        existingTransaction.Description = request.Description;

        return existingTransaction;
    }
}