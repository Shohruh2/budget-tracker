using System.Security.Claims;
using MyBudgetTracker.Models;

namespace MyBudgetTracker.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public CurrentUser GetCurrentUser()
    {
        if (_contextAccessor?.HttpContext == null)
        {
            throw new InvalidOperationException("Context accessor should not be null to get current user");
        }

        var currentUserIdString = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(currentUserIdString, out var userGuid))
        {
            throw new InvalidOperationException(
                $"User guid provided in claims is an invalid guid {currentUserIdString}");
        }

        var username = _contextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Username)?.Value;
        if (string.IsNullOrEmpty(username))
        {
            throw new InvalidOperationException($"User's username provided in claims is invalid: {username}");
        }
        
        var givenName = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value;
        if (string.IsNullOrEmpty(givenName))
        {
            throw new InvalidOperationException($"User's given name provided in claims is invalid: {givenName}");
        }
        
        var birthdate = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.DateOfBirth)?.Value;
        if (birthdate == null || !DateTime.TryParse(birthdate, out var birthdateValue))
        {
            throw new InvalidOperationException($"User's birthdate provided in claims is invalid: {birthdate}");
        }
        
        var gender = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Gender)?.Value;
        if (string.IsNullOrEmpty(gender))
        {
            throw new InvalidOperationException($"User's gender provided in claims is invalid: {gender}");
        }
        
        var email = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
        {
            throw new InvalidOperationException($"User's email provided in claims is invalid: {email}");
        }

        return new CurrentUser()
        {
            Id = userGuid,
            Name = givenName,
            Gender = gender,
            Birthday = birthdateValue
        };
    }
    
    private static class CustomClaimTypes
    {
        public const string Username = "cognito:username";
    }
}