using FluentValidation;
using MyBudgetTracker.Mapping;
using MyBudgetTracker.Models;
using MyBudgetTracker.Repositories;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;

namespace MyBudgetTracker.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<CreateTransactionRequest> _validator;

    public TransactionService(ITransactionRepository repository, IValidator<CreateTransactionRequest> validator, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _validator = validator;
        _categoryRepository = categoryRepository;
    }

    public async Task<TransactionResponse?> CreateAsync(CreateTransactionRequest request, Guid userId, CancellationToken token = default)
    {
        await _validator.ValidateAndThrowAsync(request, token);
        var transaction = request.MapToTransaction(userId);

        var category = await _categoryRepository.GetAsync(request.CategoryId, token);

        if (category == null || category.UserId != userId)
        {
            return null;
        }
        
        await _repository.CreateAsync(transaction, token);
        return transaction.MapToResponse(category);
    }
    
    
    public async Task<TransactionsResponse> GetAllGroupedAsync(Guid userId, CancellationToken token = default)
    {
        var transactions = await _repository.GetAllGroupedAsync(userId, token);
        return transactions!.MapToResponse();
    }   

    public async Task<TransactionResponse?> GetAsync(Guid transactionId, Guid userId, CancellationToken token = default)
    {
        var transaction = await _repository.GetAsync(transactionId, token);

        if (transaction == null || transaction.UserId != userId)
        {
            return null;
        }

        var category = await _categoryRepository.GetAsync(transaction.CategoryId, token);
        if (category == null)
        {
            return null;
        }

        return transaction.MapToResponse(category);
    }

    public async Task<TransactionResponse?> UpdateAsync(Guid transactionId, UpdateTransactionRequest request, Guid userId, CancellationToken token = default)
    {
        var transaction = await _repository.GetAsync(transactionId, token);
        if (transaction == null || transaction.UserId != userId)
        {
            return null;
        }
        
        transaction.Amount = request.Amount;
        transaction.Description = request.Description;

        await _repository.UpdateAsync(transaction, token);

        var category = await _categoryRepository.GetAsync(transaction.CategoryId, token);
        return transaction.MapToResponse(category);
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        return await _repository.DeleteAsync(id, userId, token);
    }
}