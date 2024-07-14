using FluentValidation;
using MyBudgetTracker.Repositories;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Services;
using MyBudgetTracker.Validators;

namespace MyBudgetTracker;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IValidator<CreateUserRequest>, CreateUserValidator>();
        services.AddTransient<IValidator<CreateCategoryDto>, CategoryValidator>();
        return services;
    }
}