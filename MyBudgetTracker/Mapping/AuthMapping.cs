using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using MyBudgetTracker.Requests;

namespace MyBudgetTracker.Mapping;

public static class AuthMapping
{
    public static SignUpRequest MapToSignUpRequest(this RegistrationRequest request, string clientId, string secretHash)
    {
        return new SignUpRequest
        {
            ClientId = clientId,
            SecretHash = secretHash,
            Username = request.Email,
            Password = request.Password,
            UserAttributes = new List<AttributeType>
            {
                new AttributeType { Name = "given_name", Value = request.Name},
                new AttributeType { Name = "email", Value = request.Email},
                new AttributeType { Name = "gender", Value = request.Gender},
                new AttributeType { Name = "birthdate", Value = request.Birthday.ToString("dd/MM/yyyy")},
            }
        };
    }
    
    public static ConfirmSignUpRequest MapToSignUpRequest(this ConfirmRegistrationRequest request, string? clientId,
        string secretHash)
    {
        return new ConfirmSignUpRequest
        {
            ClientId = clientId,
            Username = request.Username,
            ConfirmationCode = request.ConfrimationCode,
            SecretHash = secretHash
        };
    }
    
    public static AdminInitiateAuthRequest MapToInitiateAuthRequest(this LoginRequest loginRequest, string? clientId, string secretHash)
    {
        return new AdminInitiateAuthRequest
        {
            UserPoolId = "us-east-1_agpSnXpsl",
            ClientId = clientId,
            AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
            AuthParameters = new Dictionary<string, string>
            {
                {"USERNAME", loginRequest.Username},
                {"PASSWORD", loginRequest.Password},
                {"SECRET_HASH", secretHash}
            } 
        };
    }
}