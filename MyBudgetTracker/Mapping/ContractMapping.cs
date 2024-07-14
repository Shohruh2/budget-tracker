using MyBudgetTracker.Models;
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

    // public static ConfirmationResponse MapToResponse(this ConfirmRegistrationRequest request)
    // {
    //     return new ConfirmationResponse()
    //     {
    //         Username = request.Username
    //     };
    // }
}