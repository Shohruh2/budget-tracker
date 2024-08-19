using FluentValidation;
using MyBudgetTracker.Mapping;
using MyBudgetTracker.Models;
using MyBudgetTracker.Repositories;
using MyBudgetTracker.Requests;

namespace MyBudgetTracker.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IValidator<CreateUserRequest> _validator;

    public UserService(IUserRepository repository, IValidator<CreateUserRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<User> CreateAsync(CreateUserRequest request, CancellationToken token = default)
    {
        await _validator.ValidateAndThrowAsync(request, token);
        var user = request.MapToUser();
        await _repository.CreateAsync(user, token);
        return user;
    }
}