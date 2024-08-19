using System.Net;
using System.Security.Cryptography;
using System.Text;
using Amazon.CognitoIdentityProvider;
using MyBudgetTracker.Mapping;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;

namespace MyBudgetTracker.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IAmazonCognitoIdentityProvider _cognitoIdentityProvider;
    private readonly IUserService _userService;

    public AuthService(IConfiguration configuration, IAmazonCognitoIdentityProvider cognitoIdentityProvider, IUserService userService)
    {
        _configuration = configuration;
        _cognitoIdentityProvider = cognitoIdentityProvider;
        _userService = userService;
    }

    public async Task<bool> Registration(RegistrationRequest request, CancellationToken token = default)
    {
        var clientId = _configuration["AWS:ClientId"];
        var clientSecret = _configuration["AWS:ClientSecret"];
        if (clientId == null || clientSecret == null)
        {
            return false;
        }
        
        var secretHash = GenerateSecretHash(request.Email, clientId, clientSecret);
        var signUpRequest = request.MapToSignUpRequest(clientId, secretHash);

        var result = await _cognitoIdentityProvider.SignUpAsync(signUpRequest, token);
        if (result.HttpStatusCode != HttpStatusCode.OK)
        {
            return false;
        }

        var userId = result.UserSub;
        
        await _userService.CreateAsync(new CreateUserRequest
        {
            Id = Guid.Parse(userId),
            Name = request.Name,
            Gender = request.Gender,
            Birthday = request.Birthday 
        }, token);

        return true;
    }

    public async Task<bool> ConfirmRegistration(ConfirmRegistrationRequest request, CancellationToken token = default)
    {
        var clientId = _configuration["AWS:ClientId"];
        var clientSecret = _configuration["AWS:ClientSecret"];
        if (clientId == null || clientSecret == null)
        {
            return false;
        }

        var secretHash = GenerateSecretHash(request.Username, clientId, clientSecret);
        var confirmCode = request.MapToSignUpRequest(clientId, secretHash);

        var confirmResult = await _cognitoIdentityProvider.ConfirmSignUpAsync(confirmCode, token);
        if (confirmResult.HttpStatusCode != HttpStatusCode.OK)
        {
            return false;
        }

        return true;
    }

    public async Task<AuthLoginResponse?> Login(LoginRequest loginRequest, CancellationToken token = default)
    {
        var clientId = _configuration["AWS:ClientId"];
        var clientSecret = _configuration["AWS:ClientSecret"];
        if (clientId == null || clientSecret == null)
        {
            return null;
        }

        var secretHash = GenerateSecretHash(loginRequest.Username, clientId, clientSecret);
        var login = loginRequest.MapToInitiateAuthRequest(clientId, secretHash);

        try
        {
            var authResponse = await _cognitoIdentityProvider.AdminInitiateAuthAsync(login, token);
            var refreshToken = authResponse.AuthenticationResult.RefreshToken;
            var idToken = authResponse.AuthenticationResult.IdToken;
            var accessToken = authResponse.AuthenticationResult.AccessToken;
            if (authResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                return new AuthLoginResponse
                {
                    AccessToken = accessToken,
                    IdToken = idToken,
                    RefreshToken = refreshToken
                };
            }

            return null;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public string GenerateSecretHash(string username, string clientId, string clientSecret)
    {
        var message = Encoding.UTF8.GetBytes(username + clientId);
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(clientSecret));
        var hash = hmac.ComputeHash(message);
        return Convert.ToBase64String(hash);
    }
}